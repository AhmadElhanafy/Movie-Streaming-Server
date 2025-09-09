using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Api.Domain;

var builder = WebApplication.CreateBuilder(args);

// ===== 1. Setup CORS =====
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("frontend.movieapp.svc.cluster.local")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// ===== 2. Connect to Postgres inside k8s =====
var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// ===== 3. Configure JWT Authentication =====
var jwtKey = builder.Configuration["Jwt:Key"] ?? "ReplaceWithYourStrongSecretKey!";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "movieapp";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "movieapp";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// ===== 4. Add Authorization =====
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("PremiumUser", policy => policy.RequireClaim("SubscriptionLevel", "Premium"));
});

// ===== 5. Add Controllers and Swagger =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ===== 6. Apply Migrations =====
if (args.Length > 0 && args[0] == "migrate")
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var dbContext = services.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
            Console.WriteLine("Database migrations applied successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
            Environment.Exit(1);
        }
    }
    Environment.Exit(0);
}

// ===== 7. Swagger (Development only) =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var url = "http://localhost:5169/swagger";
    await Task.Run(() =>
    {
        try
        {
            Thread.Sleep(1000);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch { }
    });
}

// ===== 8. Middleware =====
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ===== 9. Seed Admin User and Roles =====
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!dbContext.Users.Any(u => u.Username == "admin"))
    {
        var admin = new User
        {
            Username = "admin",
            DisplayName = "Administrator",
            Role = "Admin"
        };
        admin.SetPassword("Admin123!"); // Strong password
        dbContext.Users.Add(admin);
        await dbContext.SaveChangesAsync();
        Console.WriteLine("Admin user created successfully.");
    }
}

app.Run();
