namespace MediCore.Domain.Entities;

public class Appointment : BaseEntity
{
    public string AppointmentNumber { get; set; } = string.Empty; // APT001, APT002, etc.
    public DateTime AppointmentDate { get; set; }
    public TimeSpan AppointmentTime { get; set; }
    public int DurationMinutes { get; set; } = 15;
    public string AppointmentType { get; set; } = "New"; // New, Follow-up, Emergency, Video
    public string Status { get; set; } = "Scheduled"; // Scheduled, Confirmed, Checked-In, In Progress, Completed, Cancelled, No-Show
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public bool ReminderSent { get; set; } = false;
    public DateTime? ReminderSentAt { get; set; }
    
    // Foreign keys
    public Guid PatientId { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? CreatedById { get; set; }
    
    // Navigation properties
    public Patient Patient { get; set; } = null!;
    public User? Doctor { get; set; }
    public User? CreatedBy { get; set; }
}
