using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestionPresentateur.Models;
using Microsoft.AspNetCore.Identity;

namespace GestionPresentateur.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Presentateur> Presentateurs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Numero> Numeros { get; set; }
        public DbSet<Performance> Performances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Presentateur: Relationship with Role
            modelBuilder.Entity<Presentateur>()
                .HasOne(p => p.Role)
                .WithMany(r => r.Presentateurs)
                .HasForeignKey(p => p.CodeR)
                .OnDelete(DeleteBehavior.Cascade);

            // Numero: Relationship with Presentateur
            modelBuilder.Entity<Numero>()
                .HasOne(n => n.Presentateur)
                .WithMany()
                .HasForeignKey(n => n.CodeP)
                .OnDelete(DeleteBehavior.Cascade);

            // Performance: Relationships with Numero and IdentityUser
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Numero)
                .WithMany()
                .HasForeignKey(p => p.CodeN)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Compte)
                .WithMany()
                .HasForeignKey(p => p.CompteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}