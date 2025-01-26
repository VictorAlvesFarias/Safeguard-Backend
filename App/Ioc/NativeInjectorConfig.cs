
using App.Extensions;
using Application.Services;
using Application.Services.AppFileService;
using Application.Services.EmailService;
using Application.Services.Identity;
using Domain.Entitites;
using Infrastructure.Context;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Api.Extensions;
using Safeguard.Services;

namespace ASP.NET_Core_Template.Ioc
{
    public static class NativeInjectorConfig
    {
        //Injeção das dependecias
        public static void RegisterServices( this IServiceCollection services, IConfiguration configuration)
        {   
            services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IPlatformService, PlatformService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAppFileService, AppFileService>();
            services.AddScoped<IRecoveryKeyService, RecoveryKeyService>();
            services.AddScoped<IRecoveryEmailService, RecoveryEmailService>();

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowedCorsOrigins",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddAuthentication(configuration);
            services.AddDbContext<ApplicationContext>(opt =>
            {
                var connectionString = $"{configuration.GetConnectionString("DefaultConnection")}Password={Environment.GetEnvironmentVariable("DEVELOPMENT_DATABASE_KEY")};";
                var connectionStringSqLite = "Data Source = App.db";

                opt.UseSqlite(connectionStringSqLite);
            });
        }
    }
}
