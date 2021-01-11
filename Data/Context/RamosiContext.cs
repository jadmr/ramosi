using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class RamosiContext : DbContext
    {
        public RamosiContext() : base()
        {
        }

        public RamosiContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<GoogleAuth> GoogleAuths { get; set; }

        public DbSet<Plant> Plants { get; set; }

        public DbSet<PlantCharacteristic> PlantCharacteristics { get; set; }

        public DbSet<PlantCollection> PlantCollections { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<WateringSchedule> WateringSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoogleAuth>(g => {
                g.HasKey(g => g.Guid);

                g.Property(g => g.Sub)
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<Plant>(p => {
                p.HasKey(p => p.Guid);

                p.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                p.HasOne(p => p.PlantCharacteristic)
                    .WithMany(c => c.Plants);
            });

            modelBuilder.Entity<PlantCharacteristic>(c => {
                c.HasKey(c => c.Guid);

                c.Property(c => c.Notes)
                    .HasMaxLength(8000);
            });

            modelBuilder.Entity<PlantCollection>(c => {
                c.HasKey(c => c.Guid);

                c.Property(c => c.Nickname)
                    .HasMaxLength(100);

                c.Property(c => c.Location)
                    .HasMaxLength(100);

                c.Property(c => c.Notes)
                    .HasMaxLength(8000);

                c.HasOne(c => c.User)
                    .WithMany(u => u.PlantCollection)
                    .HasForeignKey(c => c.UserId);

                c.HasOne(c => c.Plant)
                    .WithMany(p => p.PlantCollection)
                    .HasForeignKey(c => c.PlantId);
            });

            modelBuilder.Entity<User>(u => {
                u.HasKey(u => u.Guid);

                u.Property(u => u.Nickname)
                    .IsRequired()
                    .HasMaxLength(50);

                u.Property(u => u.Email)
                    .HasMaxLength(254);

                u.Property(u => u.Phone)
                    .HasMaxLength(50);

                u.HasOne(u => u.GoogleAuth)
                    .WithOne(g => g.User)
                    .HasForeignKey<GoogleAuth>(g => g.UserId);
            });

            modelBuilder.Entity<WateringSchedule>(w => {
                w.HasKey(w => w.Guid);

                w.Property(w => w.DayOfWeek)
                    .IsRequired()
                    .HasMaxLength(25);

                w.Property(w => w.TimeOfDay)
                    .IsRequired();

                w.Property(w => w.Repeat)
                    .IsRequired()
                    .HasMaxLength(25);

                w.HasOne(w => w.PlantCollection)
                    .WithMany(c => c.Schedules);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;database=ramosi_dev_db;user=ramosi_dev;password=2BeRamosiMySqlDevAccountMe");
            }
        }
    }
}