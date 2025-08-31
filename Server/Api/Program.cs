using Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connect to Postgres inside k8s
var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Auto-open Swagger UI in browser
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
        catch { /* ignore errors */ }
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
