using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediCore.API.Models
{
    public class Billing
    {
        [Key]
        public int BillingId { get; set; }
        
        [Required]
        public int VisitId { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal ConsultationFee { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal ProcedureCharges { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }
        
        [MaxLength(50)]
        public string PaymentMode { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string PaymentStatus { get; set; } = "Pending";
        
        public DateTime? PaymentDate { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("VisitId")]
        public Visit Visit { get; set; } = null!;
    }
}
