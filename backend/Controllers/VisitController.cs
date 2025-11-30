using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore.API.Data;
using MediCore.API.Models;

namespace MediCore.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VisitController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public VisitController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<Visit>>> GetPatientVisits(int patientId)
        {
            var visits = await _context.Visits
                .Where(v => v.PatientId == patientId)
                .Include(v => v.Prescription)
                .Include(v => v.Billing)
                .OrderByDescending(v => v.VisitDate)
                .ToListAsync();
                
            return Ok(visits);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Visit>> GetVisit(int id)
        {
            var visit = await _context.Visits
                .Include(v => v.Patient)
                .Include(v => v.Prescription)
                .Include(v => v.Billing)
                .FirstOrDefaultAsync(v => v.VisitId == id);
                
            if (visit == null)
                return NotFound();
                
            return Ok(visit);
        }
        
        [HttpPost]
        public async Task<ActionResult<Visit>> CreateVisit(Visit visit)
        {
            _context.Visits.Add(visit);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetVisit), new { id = visit.VisitId }, visit);
        }
        
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateVisit(int id, Visit visit)
        {
            if (id != visit.VisitId)
                return BadRequest();
                
            _context.Entry(visit).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await VisitExists(id))
                    return NotFound();
                throw;
            }
            
            return NoContent();
        }
        
        [HttpGet("followups/today")]
        public async Task<ActionResult<IEnumerable<Visit>>> GetTodayFollowups()
        {
            var today = DateTime.UtcNow.Date;
            
            var followups = await _context.Visits
                .Include(v => v.Patient)
                .Where(v => v.FollowUpDate.HasValue && v.FollowUpDate.Value.Date == today)
                .ToListAsync();
                
            return Ok(followups);
        }
        
        private async Task<bool> VisitExists(int id)
        {
            return await _context.Visits.AnyAsync(e => e.VisitId == id);
        }
    }
}
