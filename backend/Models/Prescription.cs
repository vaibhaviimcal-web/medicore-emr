using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediCore.API.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }
        
        [Required]
        public int VisitId { get; set; }
        
        public List<PrescribedMedicine> Medicines { get; set; } = new();
        
        public string? Instructions { get; set; }
        
        public string? PdfPath { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("VisitId")]
        public Visit Visit { get; set; } = null!;
    }
    
    public class PrescribedMedicine
    {
        public string Name { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
    }
}
