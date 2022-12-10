using TimeLogger.Domain.Models;

namespace TimeLogger.Domain;

public class TimeLoggerDbContext : DbContext
{
    public DbSet<Client> Client { get; set; }
    public DbSet<Default> Default { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Work> Work { get; set; }

    public string DbPath { get; }

    public TimeLoggerDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "timeLogger.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Client Table
        modelBuilder.Entity<Client>()
                    .Property(c => c.PreBill)
                    .HasDefaultValue(false);
        modelBuilder.Entity<Client>()
                    .Property(c => c.HasCutOff)
                    .HasDefaultValue(false);
        modelBuilder.Entity<Client>()
                    .Property(c => c.MinimumHours)
                    .HasDefaultValue(0);
        modelBuilder.Entity<Client>()
                    .Property(c => c.BillingIncrement)
                    .HasDefaultValue(0.25f);
        modelBuilder.Entity<Client>()
                    .Property(c => c.RoundUpAfterXMinutes)
                    .HasDefaultValue(0);
        // Default Table
        modelBuilder.Entity<Default>()
                    .Property(c => c.PreBill)
                    .HasDefaultValue(false);
        modelBuilder.Entity<Default>()
                    .Property(c => c.HasCutOff)
                    .HasDefaultValue(false);
        modelBuilder.Entity<Default>()
                    .Property(c => c.CutOff)
                    .HasDefaultValue(0);
        modelBuilder.Entity<Default>()
                    .Property(c => c.MinimumHours)
                    .HasDefaultValue(0);
        modelBuilder.Entity<Default>()
                    .Property(c => c.BillingIncrement)
                    .HasDefaultValue(0.25f);
        modelBuilder.Entity<Default>()
                    .Property(c => c.RoundUpAfterXMinutes)
                    .HasDefaultValue(0);

        // Payment Table
        modelBuilder.Entity<Payment>()
                    .Property(p => p.Date)
                    .HasDefaultValueSql("(datetime('now', 'localtime'))");

        // Work Table
        modelBuilder.Entity<Work>()
                    .Property(w => w.DateEntered)
                    .HasDefaultValueSql("(datetime('now', 'localtime'))");

    }
}
