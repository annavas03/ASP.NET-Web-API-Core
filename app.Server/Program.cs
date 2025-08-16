using app.Server.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
try
{
    Console.WriteLine("Starting app...");
    var app = builder.Build();

    app.UseCors("AllowReact");
    app.MapControllers();

    Console.WriteLine("Running app...");
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Exception: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
}