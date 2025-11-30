namespace MediCore.Domain.Entities;

public class Visit : BaseEntity
{
    public string VisitNumber { get; set; } = string.Empty; // V001, V002, etc.
    public DateTime VisitDate { get; set; } = DateTime.UtcNow.Date;
    public TimeSpan VisitTime { get; set; } = DateTime.UtcNow.TimeOfDay;
    public string VisitType { get; set; } = "New"; // New, Follow-up, Emergency
    public string Status { get; set; } = "Pending"; // Pending, In Progress, Completed, Cancelled
    
    // Vitals
    public int? BpSystolic { get; set; }
    public int? BpDiastolic { get; set; }
    public decimal? Temperature { get; set; }
    public int? Pulse { get; set; }
    public int? SpO2 { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public decimal? Bmi { get; set; }
    
    // Clinical Data
    public string? ChiefComplaints { get; set; }
    public string? HistoryPresentIllness { get; set; }
    public string? PastMedicalHistory { get; set; }
    public string? FamilyHistory { get; set; }
    public string? PersonalHistory { get; set; }
    public string? ExaminationFindings { get; set; }
    public string? ProvisionalDiagnosis { get; set; }
    public string? FinalDiagnosis { get; set; }
    public string? InvestigationOrders { get; set; }
    public string? TreatmentPlan { get; set; }
    public string? Advice { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public string? FollowUpNotes { get; set; }
    
    // Metadata
    public int? DurationMinutes { get; set; }
    public string? Notes { get; set; }
    
    // Foreign keys
    public Guid PatientId { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? CreatedById { get; set; }
    
    // Navigation properties
    public Patient Patient { get; set; } = null!;
    public User? Doctor { get; set; }
    public User? CreatedBy { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    public ICollection<LabOrder> LabOrders { get; set; } = new List<LabOrder>();
}
