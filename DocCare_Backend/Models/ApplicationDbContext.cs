using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DocCare_Backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Docteur> Docteurs { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<DossierMedical> DossiersMedicaux { get; set; }



<<<<<<< HEAD
=======
        public DbSet<Specialite> Specialites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration de la relation One-to-Many
            modelBuilder.Entity<Docteur>()
                .HasOne(d => d.Specialite)
                .WithMany(s => s.Docteurs)
                .HasForeignKey(d => d.SpecialiteId);
        }
>>>>>>> 82dbf883f429addd85c850017030f3ea457b1a36

    }

}
