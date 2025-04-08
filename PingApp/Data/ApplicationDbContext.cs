using Microsoft.EntityFrameworkCore;
using PingApp.Models;

namespace PingApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<DeviceGroup> DeviceGroups { get; set; } = null!;
        public DbSet<Device> Devices { get; set; } = null!;
        public DbSet<PingHistory> PingHistories { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasOne(d => d.Category)
                .WithMany(c => c.Devices)
                .HasForeignKey(d => d.CategoryId);

            modelBuilder.Entity<Device>()
                .HasOne(d => d.Group)
                .WithMany(g => g.Devices)
                .HasForeignKey(d => d.GroupId);

            modelBuilder.Entity<PingHistory>()
                .HasOne(ph => ph.Device)
                .WithMany(d => d.PingHistories)
                .HasForeignKey(ph => ph.DeviceId);
        }
    }

}
