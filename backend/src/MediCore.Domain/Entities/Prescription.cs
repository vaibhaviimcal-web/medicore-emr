namespace MediCore.Domain.Entities;

public class Prescription : BaseEntity
{
    public string PrescriptionNumber { get; set; } = string.Empty; // RX001, RX002, etc.
    public DateTime PrescriptionDate { get; set; } = DateTime.UtcNow.Date;
    public string? Diagnosis { get; set; }
    public string? Advice { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public string Status { get; set; } = "Active"; // Active, Completed, Cancelled
    public string? PdfUrl { get; set; }
    public bool SentViaWhatsApp { get; set; } = false;
    public DateTime? WhatsAppSentAt { get; set; }
    
    // Foreign keys
    public Guid? VisitId { get; set; }
    public Guid PatientId { get; set; }
    public Guid? DoctorId { get; set; }
    
    // Navigation properties
    public Visit? Visit { get; set; }
    public Patient Patient { get; set; } = null!;
    public User? Doctor { get; set; }
    public ICollection<PrescriptionItem> Items { get; set; } = new List<PrescriptionItem>();
}

public class PrescriptionItem : BaseEntity
{
    public string MedicineName { get; set; } = string.Empty;
    public string? Dosage { get; set; }
    public string? Frequency { get; set; }
    public string? Duration { get; set; }
    public string? Quantity { get; set; }
    public string? Instructions { get; set; }
    public string? Timing { get; set; } // Before food, After food, etc.
    public string? Route { get; set; } // Oral, Topical, etc.
    public int DisplayOrder { get; set; }
    
    // Foreign key
    public Guid PrescriptionId { get; set; }
    
    // Navigation property
    public Prescription Prescription { get; set; } = null!;
}
