using Domain.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public readonly IHttpContextAccessor _contextAccessor;
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<AppFile> AppFile { get; set; }
        public DbSet<Platform> Platform { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            _contextAccessor = contextAccessor;
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
        }

    }
}
