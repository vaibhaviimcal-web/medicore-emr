using System.ComponentModel.DataAnnotations;

namespace MediCore.API.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string PatientCode { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public int Age { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string Gender { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(15)]
        public string Phone { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? Email { get; set; }
        
        public string? Address { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }
}
