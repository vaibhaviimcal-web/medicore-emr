namespace MediCore.Domain.Entities;

public class Bill : BaseEntity
{
    public string BillNumber { get; set; } = string.Empty; // BILL001, BILL002, etc.
    public DateTime BillDate { get; set; } = DateTime.UtcNow.Date;
    public decimal Subtotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TaxPercentage { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal BalanceAmount { get; set; }
    public string PaymentStatus { get; set; } = "Pending"; // Pending, Partial, Paid, Cancelled
    public string? PaymentMode { get; set; } // Cash, Card, UPI, Net Banking, Insurance
    public string? Notes { get; set; }
    
    // Foreign keys
    public Guid? VisitId { get; set; }
    public Guid PatientId { get; set; }
    public Guid? CreatedById { get; set; }
    
    // Navigation properties
    public Visit? Visit { get; set; }
    public Patient Patient { get; set; } = null!;
    public User? CreatedBy { get; set; }
    public ICollection<BillItem> Items { get; set; } = new List<BillItem>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}

public class BillItem : BaseEntity
{
    public string ItemType { get; set; } = string.Empty; // Consultation, Procedure, Medicine, Lab
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public int DisplayOrder { get; set; }
    
    // Foreign key
    public Guid BillId { get; set; }
    
    // Navigation property
    public Bill Bill { get; set; } = null!;
}

public class Payment : BaseEntity
{
    public string PaymentNumber { get; set; } = string.Empty; // PAY001, PAY002, etc.
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow.Date;
    public decimal Amount { get; set; }
    public string PaymentMode { get; set; } = string.Empty;
    public string? TransactionId { get; set; }
    public string? Notes { get; set; }
    
    // Foreign keys
    public Guid BillId { get; set; }
    public Guid PatientId { get; set; }
    public Guid? CreatedById { get; set; }
    
    // Navigation properties
    public Bill Bill { get; set; } = null!;
    public Patient Patient { get; set; } = null!;
    public User? CreatedBy { get; set; }
}
