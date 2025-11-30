using Microsoft.EntityFrameworkCore;
using MediCore.Domain.Entities;

namespace MediCore.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionItem> PrescriptionItems { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<BillItem> BillItems { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<LabOrder> LabOrders { get; set; }
    public DbSet<LabOrderItem> LabOrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Configure table names (lowercase for PostgreSQL convention)
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<Patient>().ToTable("patients");
        modelBuilder.Entity<Visit>().ToTable("visits");
        modelBuilder.Entity<Prescription>().ToTable("prescriptions");
        modelBuilder.Entity<PrescriptionItem>().ToTable("prescription_items");
        modelBuilder.Entity<Bill>().ToTable("bills");
        modelBuilder.Entity<BillItem>().ToTable("bill_items");
        modelBuilder.Entity<Payment>().ToTable("payments");
        modelBuilder.Entity<Appointment>().ToTable("appointments");
        modelBuilder.Entity<Medicine>().ToTable("medicines");
        modelBuilder.Entity<LabOrder>().ToTable("lab_orders");
        modelBuilder.Entity<LabOrderItem>().ToTable("lab_order_items");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Auto-update UpdatedAt timestamp
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            ((BaseEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                ((BaseEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
