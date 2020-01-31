using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MedAidAPI.Models;

namespace MedAidAPI.Areas.Identity.Data
{
    public class MedAidAPIContext : IdentityDbContext<MedAidAPIUser>
    {
        public MedAidAPIContext(DbContextOptions<MedAidAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<AlarmType> AlarmTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationType> LocationTypes { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Medicine> Medicines { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=A003-00155\\SQLSERVER;Database=MedAidAPI;User ID=sa;password=1234@zubair");
        }
    }
}
