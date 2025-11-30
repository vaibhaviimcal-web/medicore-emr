using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore.API.Data;
using MediCore.API.Models;

namespace MediCore.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MasterDataController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public MasterDataController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("chief-complaints")]
        public async Task<ActionResult<IEnumerable<ChiefComplaintMaster>>> GetChiefComplaints()
        {
            var complaints = await _context.ChiefComplaintsMaster
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();
                
            return Ok(complaints);
        }
        
        [HttpGet("diagnoses")]
        public async Task<ActionResult<IEnumerable<DiagnosisMaster>>> GetDiagnoses()
        {
            var diagnoses = await _context.DiagnosesMaster
                .OrderBy(d => d.DisplayOrder)
                .ToListAsync();
                
            return Ok(diagnoses);
        }
        
        [HttpGet("medicines")]
        public async Task<ActionResult<IEnumerable<MedicineMaster>>> GetMedicines()
        {
            var medicines = await _context.MedicinesMaster
                .Where(m => m.IsFavorite)
                .OrderBy(m => m.Name)
                .ToListAsync();
                
            return Ok(medicines);
        }
    }
}
