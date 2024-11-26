using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using App.Program;
using Application.Dtos.Default;
using Application.Dtos.User.Create;
using Application.Dtos.User.Login;
using Domain.Entitites;
using System.Net;
using Test.Utils;
using System.Text.Json;

namespace Test.IntegrationTests
{
    public class BasicTests: WebApplicationFactory<Program>
    {
        public readonly string BearerToken;
        private readonly string _client;
        public BasicTests()
        {

        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices((configuration,services) =>
            {
                services.RemoveAll(typeof(DbContextOptions<ApplicationContext>));
                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseSqlServer($"{configuration.Configuration.GetConnectionString("DefaultConnection")}Password={Environment.GetEnvironmentVariable("DEVELOPMENT_DATABASE_KEY")};");
                });
            });
            builder.UseEnvironment("Development");
            base.ConfigureWebHost(builder);   
        }
    }
}
