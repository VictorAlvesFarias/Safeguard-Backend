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
        protected DbSet<AppFile> AppFile { get; set; }
        protected DbSet<Platform> Platform { get; set; }
        protected DbSet<RecoveryKey> RecoveryKey { get; set; }
        protected DbSet<EmailAddress> EmailAddress { get; set; }
        protected DbSet<EmailFile> EmailFile { get; set; }
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

            //-----------------------------------------------

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.EmailAddress)
                    .WithMany()
                    .HasForeignKey(e => e.EmailAddressId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.HasOne(e => e.Platform)
                    .WithMany()
                    .HasForeignKey(e => e.PlatformId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.HasOne(e => e.EmailAddress)
                    .WithMany()
                    .HasForeignKey(e => e.EmailAddressId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Phone).IsRequired(false);
                entity.Property(e => e.BirthDate).IsRequired();

                entity.HasIndex(e => e.Username).IsUnique();
            });

            modelBuilder.Entity<AppFile>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Email>(e =>
            {
                e.HasIndex(e => e.Username)
                    .IsUnique(false);
            });

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
