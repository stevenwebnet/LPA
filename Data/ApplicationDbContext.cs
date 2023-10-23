using LPA.Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LPA.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbSet<Pronostic> Pronostics { get; set; }
        public DbSet<Rencontre> Rencontre { get; set; }
        public DbSet<Competition> Competition { get; set; }
        public DbSet<Tournoi> Tournoi { get; set; }
        public DbSet<Equipe> Equipe { get; set; }
        public DbSet<Lieu> Lieu { get; set; }
        public DbSet<Phase> Phase { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().Ignore(c => c.AccessFailedCount)
                                       .Ignore(c => c.LockoutEnabled)
                                       .Ignore(c => c.LockoutEnd)
                                       .Ignore(c => c.TwoFactorEnabled)
                                       .Ignore(c => c.PhoneNumber)
                                       .Ignore(c => c.PhoneNumberConfirmed)
                                       .ToTable("ApplicationUser");

            builder.Entity<IdentityUserClaim<string>>().ToTable("ApplicationUserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("ApplicationUserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("ApplicationUserTokens");
            builder.Entity<IdentityUserRole<string>>().ToTable("ApplicationUserRoles");

            builder.Entity<Rencontre>()
                .HasOne(m => m.EquipeDomicile)
                .WithMany()
                .HasForeignKey(m => m.EquipeDomicileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rencontre>()
                .HasOne(m => m.EquipeExterieur)
                .WithMany()
                .HasForeignKey(m => m.EquipeExterieurId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}