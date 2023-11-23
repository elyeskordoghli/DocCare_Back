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
        public DbSet<Specialite> Specialites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configuration des relations
            modelBuilder.Entity<Docteur>()
                .HasOne(d => d.Specialite)
                .WithMany(s => s.Docteurs)
                .HasForeignKey(d => d.SpecialiteId);

            modelBuilder.Entity<Consultation>()
                .HasOne(c => c.Patient)
                .WithMany()
                .HasForeignKey(c => c.PatientId);



        }

    }
}
