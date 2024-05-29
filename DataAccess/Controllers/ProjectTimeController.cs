using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTimeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectTimeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTime>>> GetProjectTimes()
        {
            return await _context.ProjectTimes
                .Include(pt => pt.User)
                .Include(pt => pt.Project)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTime>> GetProjectTime(int id)
        {
            var projectTime = await _context.ProjectTimes
                .Include(pt => pt.User)
                .Include(pt => pt.Project)
                .FirstOrDefaultAsync(pt => pt.Id == id);

            if (projectTime == null)
            {
                return NotFound();
            }

            return projectTime;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectTime(int id, ProjectTime projectTime)
        {
            if (id != projectTime.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTimeExists(id))
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
        public async Task<ActionResult<ProjectTime>> PostProjectTime(ProjectTime projectTime)
        {
            _context.ProjectTimes.Add(projectTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectTime", new { id = projectTime.Id }, projectTime);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTime(int id)
        {
            var projectTime = await _context.ProjectTimes.FindAsync(id);
            if (projectTime == null)
            {
                return NotFound();
            }

            _context.ProjectTimes.Remove(projectTime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectTimeExists(int id)
        {
            return _context.ProjectTimes.Any(e => e.Id == id);
        }
    }
}
