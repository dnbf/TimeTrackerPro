using Microsoft.EntityFrameworkCore;
using TimeTrackerPro.Domain.Entities;

namespace TimeTrackerPro.Infrastructure.Persistence
{
    public class TimeTrackerDbContext : DbContext
    {
        public TimeTrackerDbContext(DbContextOptions<TimeTrackerDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<ActivityEntry> ActivityEntries => Set<ActivityEntry>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(180);

                entity.Property(x => x.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(x => x.CreatedAt)
                    .IsRequired();

                entity.HasIndex(x => x.Email)
                    .IsUnique();
            });

            modelBuilder.Entity<ActivityEntry>(entity =>
            {
                entity.ToTable("ActivityEntries");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Date)
                    .IsRequired();

                entity.Property(x => x.StartTime)
                    .IsRequired();

                entity.Property(x => x.EndTime)
                    .IsRequired();

                entity.Property(x => x.DurationMinutes)
                    .IsRequired();

                entity.Property(x => x.Category)
                    .IsRequired();

                entity.Property(x => x.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(x => x.CreatedAt)
                    .IsRequired();

                entity.HasIndex(x => x.UserId);
                entity.HasIndex(x => x.Date);
            });
        }
    }
}
