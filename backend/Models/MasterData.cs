using System.ComponentModel.DataAnnotations;

namespace MediCore.API.Models
{
    public class ChiefComplaintMaster
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Complaint { get; set; } = string.Empty;
        
        public int DisplayOrder { get; set; }
    }
    
    public class DiagnosisMaster
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Diagnosis { get; set; } = string.Empty;
        
        public int DisplayOrder { get; set; }
    }
    
    public class MedicineMaster
    {
        [Key]
        public int MedicineId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string? GenericName { get; set; }
        
        [MaxLength(100)]
        public string? Dosage { get; set; }
        
        [MaxLength(50)]
        public string? Frequency { get; set; }
        
        [MaxLength(50)]
        public string? CommonDuration { get; set; }
        
        [MaxLength(200)]
        public string? Instructions { get; set; }
        
        public bool IsFavorite { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
