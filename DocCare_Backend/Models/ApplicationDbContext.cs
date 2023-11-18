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

    }

}
