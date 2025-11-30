using Microsoft.EntityFrameworkCore;
using MediCore.API.Models;

namespace MediCore.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<ChiefComplaintMaster> ChiefComplaintsMaster { get; set; }
        public DbSet<DiagnosisMaster> DiagnosesMaster { get; set; }
        public DbSet<MedicineMaster> MedicinesMaster { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure JSON columns for PostgreSQL
            modelBuilder.Entity<Visit>()
                .Property(v => v.ChiefComplaints)
                .HasColumnType("jsonb");
                
            modelBuilder.Entity<Visit>()
                .Property(v => v.Vitals)
                .HasColumnType("jsonb");
                
            modelBuilder.Entity<Visit>()
                .Property(v => v.Diagnosis)
                .HasColumnType("jsonb");
                
            modelBuilder.Entity<Prescription>()
                .Property(p => p.Medicines)
                .HasColumnType("jsonb");
            
            // Seed master data
            modelBuilder.Entity<ChiefComplaintMaster>().HasData(
                new ChiefComplaintMaster { Id = 1, Complaint = "Fever", DisplayOrder = 1 },
                new ChiefComplaintMaster { Id = 2, Complaint = "Cough", DisplayOrder = 2 },
                new ChiefComplaintMaster { Id = 3, Complaint = "Breathless", DisplayOrder = 3 },
                new ChiefComplaintMaster { Id = 4, Complaint = "Joint pains", DisplayOrder = 4 },
                new ChiefComplaintMaster { Id = 5, Complaint = "Abdominal pain", DisplayOrder = 5 },
                new ChiefComplaintMaster { Id = 6, Complaint = "General weakness", DisplayOrder = 6 }
            );
            
            modelBuilder.Entity<DiagnosisMaster>().HasData(
                new DiagnosisMaster { Id = 1, Diagnosis = "Viral fever", DisplayOrder = 1 },
                new DiagnosisMaster { Id = 2, Diagnosis = "Diabetes", DisplayOrder = 2 },
                new DiagnosisMaster { Id = 3, Diagnosis = "Hypertension", DisplayOrder = 3 },
                new DiagnosisMaster { Id = 4, Diagnosis = "CKD", DisplayOrder = 4 }
            );
            
            modelBuilder.Entity<MedicineMaster>().HasData(
                new MedicineMaster { MedicineId = 1, Name = "Crocin", Dosage = "500mg", Frequency = "TDS", CommonDuration = "3 days", Instructions = "After food", IsFavorite = true },
                new MedicineMaster { MedicineId = 2, Name = "Voveran", Dosage = "50mg", Frequency = "BD", CommonDuration = "5 days", Instructions = "After food", IsFavorite = true },
                new MedicineMaster { MedicineId = 3, Name = "Glykind-M", Dosage = "500mg", Frequency = "BD", CommonDuration = "30 days", Instructions = "Before food", IsFavorite = true },
                new MedicineMaster { MedicineId = 4, Name = "Cap Omez", Dosage = "20mg", Frequency = "OD", CommonDuration = "7 days", Instructions = "Before food", IsFavorite = true },
                new MedicineMaster { MedicineId = 5, Name = "Becasule", Dosage = "1 cap", Frequency = "OD", CommonDuration = "30 days", Instructions = "After food", IsFavorite = true }
            );
        }
    }
}
