using Domain.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.Claims;


namespace Infrastructure.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        protected DbSet<Provider> Provider { get; set; }
        protected DbSet<Email> Email { get; set; }
        protected DbSet<Account> Account { get; set; }
        protected DbSet<AppFile> AppFile { get; set; }
        protected DbSet<Platform> Platform { get; set; }
        protected DbSet<RecoveryKey> RecoveryKey { get; set; }
        protected DbSet<RecoveryEmail> RecoveryEmail { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            _contextAccessor = contextAccessor;
        }
        public string GetUserId()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Platform>()
                .HasOne(e => e.Image)
                .WithMany()
                .HasForeignKey(e => e.ImageId);

            modelBuilder.Entity<Provider>()
                .HasOne(e => e.Image)
                .WithMany()
                .HasForeignKey(e => e.ImageId); 

            modelBuilder.Entity<Account>()
                .HasOne(e => e.Email)
                .WithMany()
                .HasForeignKey(e => e.EmailId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Email>()
                .HasOne(e => e.Provider)
                .WithMany()
                .HasForeignKey(e => e.ProviderId);

            modelBuilder.Entity<Account>()
                .HasOne(e => e.Platform)
                .WithMany()
                .HasForeignKey(e => e.PlatformId)
                .OnDelete(DeleteBehavior.NoAction);
            //-----------------------------------------------
            modelBuilder.Entity<Account>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e=>e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Email>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AppFile>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Platform>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Provider>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
