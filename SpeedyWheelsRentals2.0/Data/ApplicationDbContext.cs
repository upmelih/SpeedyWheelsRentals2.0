using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpeedyWheelsRentals.Models;

namespace SpeedyWheelsRentals2._0.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SpeedyWheelsRentals.Models.Customer>? Customer { get; set; }
        public DbSet<SpeedyWheelsRentals.Models.Vehicle>? Vehicle { get; set; }
        public DbSet<SpeedyWheelsRentals.Models.Reservation>? Reservation { get; set; }
    }
}
