var builder = WebApplication.CreateBuilder(args);

// Add services to the container
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
    await Task.Run(() => {
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

app.Run();
