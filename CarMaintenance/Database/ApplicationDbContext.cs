using CarMaintenance.Models;
using Microsoft.EntityFrameworkCore;

namespace CarMaintenance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<Car> Cars { get; set; }

        public DbSet<OilChange> OilChanges { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasMany(car => car.OilChanges)
                .WithOne()
                .HasForeignKey(oil => oil.Vin)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
