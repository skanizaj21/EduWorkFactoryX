using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTimeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkTimeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkTime>>> GetWorkTimes()
        {
            return await _context.WorkTimes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkTime>> GetWorkTime(int id)
        {
            var workTime = await _context.WorkTimes.FindAsync(id);

            if (workTime == null)
            {
                return NotFound();
            }

            return workTime;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkTime(int id, WorkTime workTime)
        {
            if (id != workTime.Id)
            {
                return BadRequest();
            }

            _context.Entry(workTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkTimeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<WorkTime>> PostWorkTime(WorkTime workTime)
        {
            _context.WorkTimes.Add(workTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkTime", new { id = workTime.Id }, workTime);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkTime(int id)
        {
            var workTime = await _context.WorkTimes.FindAsync(id);
            if (workTime == null)
            {
                return NotFound();
            }

            _context.WorkTimes.Remove(workTime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkTimeExists(int id)
        {
            return _context.WorkTimes.Any(e => e.Id == id);
        }
    }
}
