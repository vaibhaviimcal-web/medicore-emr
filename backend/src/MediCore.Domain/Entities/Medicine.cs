namespace MediCore.Domain.Entities;

public class Medicine : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? GenericName { get; set; }
    public string? BrandName { get; set; }
    public string? Category { get; set; }
    public string? DosageForm { get; set; } // Tablet, Capsule, Syrup, Injection, etc.
    public string? Strength { get; set; }
    public string? Manufacturer { get; set; }
    public string? Description { get; set; }
    public string? CommonDosage { get; set; }
    public string? CommonFrequency { get; set; }
    public string? CommonDuration { get; set; }
    public string? Contraindications { get; set; }
    public string? SideEffects { get; set; }
    public string? Interactions { get; set; }
    public bool IsFavorite { get; set; } = false;
    public int UsageCount { get; set; } = 0;
}

public class LabOrder : BaseEntity
{
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow.Date;
    public string Status { get; set; } = "Ordered"; // Ordered, Sample Collected, In Progress, Completed, Cancelled
    public string? Notes { get; set; }
    
    // Foreign keys
    public Guid VisitId { get; set; }
    public Guid PatientId { get; set; }
    public Guid? DoctorId { get; set; }
    
    // Navigation properties
    public Visit Visit { get; set; } = null!;
    public Patient Patient { get; set; } = null!;
    public User? Doctor { get; set; }
    public ICollection<LabOrderItem> Items { get; set; } = new List<LabOrderItem>();
}

public class LabOrderItem : BaseEntity
{
    public string TestName { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public string? ResultValue { get; set; }
    public string? ResultUnit { get; set; }
    public string? NormalRange { get; set; }
    public bool IsAbnormal { get; set; } = false;
    public DateTime? ResultDate { get; set; }
    public string? Notes { get; set; }
    
    // Foreign key
    public Guid LabOrderId { get; set; }
    
    // Navigation property
    public LabOrder LabOrder { get; set; } = null!;
}
