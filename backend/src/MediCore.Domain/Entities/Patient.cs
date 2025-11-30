namespace MediCore.Domain.Entities;

public class Patient : BaseEntity
{
    public string PatientId { get; set; } = string.Empty; // MED001, MED002, etc.
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; } // Male, Female, Other
    public string? BloodGroup { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Pincode { get; set; }
    public string? EmergencyContactName { get; set; }
    public string? EmergencyContactPhone { get; set; }
    public string? Allergies { get; set; }
    public string? ChronicConditions { get; set; }
    public string? PhotoUrl { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Foreign keys
    public Guid? CreatedById { get; set; }
    
    // Navigation properties
    public User? CreatedBy { get; set; }
    public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    public ICollection<Bill> Bills { get; set; } = new List<Bill>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
