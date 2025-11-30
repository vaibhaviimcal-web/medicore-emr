namespace MediCore.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = "doctor"; // doctor, receptionist, nurse, admin
    public string? Phone { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? LastLogin { get; set; }
    
    // Navigation properties
    public ICollection<Patient> CreatedPatients { get; set; } = new List<Patient>();
    public ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
