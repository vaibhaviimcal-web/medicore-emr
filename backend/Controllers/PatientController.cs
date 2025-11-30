using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore.API.Data;
using MediCore.API.Models;

namespace MediCore.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var patients = await _context.Patients
                .OrderByDescending(p => p.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
                
            return Ok(patients);
        }
        
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Patient>>> SearchPatients([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return BadRequest("Search query cannot be empty");
                
            var patients = await _context.Patients
                .Where(p => p.Name.Contains(q) || p.Phone.Contains(q) || p.PatientCode.Contains(q))
                .OrderByDescending(p => p.CreatedDate)
                .Take(20)
                .ToListAsync();
                
            return Ok(patients);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Visits)
                .FirstOrDefaultAsync(p => p.PatientId == id);
                
            if (patient == null)
                return NotFound();
                
            return Ok(patient);
        }
        
        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            var lastPatient = await _context.Patients
                .OrderByDescending(p => p.PatientId)
                .FirstOrDefaultAsync();
                
            var nextId = (lastPatient?.PatientId ?? 0) + 1;
            patient.PatientCode = $"P{nextId:D6}";
            
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, patient);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.PatientId)
                return BadRequest();
                
            patient.UpdatedDate = DateTime.UtcNow;
            _context.Entry(patient).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PatientExists(id))
                    return NotFound();
                throw;
            }
            
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return NotFound();
                
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
        
        private async Task<bool> PatientExists(int id)
        {
            return await _context.Patients.AnyAsync(e => e.PatientId == id);
        }
    }
}
