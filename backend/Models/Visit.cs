using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediCore.API.Models
{
    public class Visit
    {
        [Key]
        public int VisitId { get; set; }
        
        [Required]
        public int PatientId { get; set; }
        
        public DateTime VisitDate { get; set; } = DateTime.UtcNow;
        
        public List<string> ChiefComplaints { get; set; } = new();
        
        public Vitals? Vitals { get; set; }
        
        public string? History { get; set; }
        
        public string? Examination { get; set; }
        
        public List<string> Diagnosis { get; set; } = new();
        
        public string? Notes { get; set; }
        
        public DateTime? FollowUpDate { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } = null!;
        
        public Prescription? Prescription { get; set; }
        
        public Billing? Billing { get; set; }
    }
    
    public class Vitals
    {
        public string? BP { get; set; }
        public string? Temperature { get; set; }
        public string? Pulse { get; set; }
        public string? SpO2 { get; set; }
        public string? Weight { get; set; }
    }
}
