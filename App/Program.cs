using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_Template.Ioc;
using Infrastructure.Context;
using PicEnfermagem.Api.Extensions;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using App.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSwagger();

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedCorsOrigins",
    builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddAuthentication(builder.Configuration);

builder.Services.AddDbContext<ApplicationContext>(opt =>
{
    //var connectionString = $"{configuration.GetConnectionString("DefaultConnection")}Password={Environment.GetEnvironmentVariable("DEVELOPMENT_DATABASE_KEY")};";
    var connectionString = $"{builder.Configuration.GetConnectionString("DefaultConnection")};";

    opt.UseSqlite(connectionString);
});

builder.WebHost.ConfigureKestrel(options =>
{
    var portConfig = builder.Configuration.GetSection("Ports").Get<Dictionary<string, int>>();

    options.ListenLocalhost(portConfig["Https"], configure =>
    {
        configure.UseHttps();
    });
    options.ListenLocalhost(portConfig["Http"]);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowedCorsOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace App.Program
{
    public partial class Program { }
}
