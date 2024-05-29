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
    public class ProjectRoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectRoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectRole>>> GetProjectRoles()
        {
            return await _context.ProjectRoles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectRole>> GetProjectRole(int id)
        {
            var projectRole = await _context.ProjectRoles.FindAsync(id);

            if (projectRole == null)
            {
                return NotFound();
            }

            return projectRole;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectRole(int id, ProjectRole projectRole)
        {
            if (id != projectRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectRoleExists(id))
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
        public async Task<ActionResult<ProjectRole>> PostProjectRole(ProjectRole projectRole)
        {
            _context.ProjectRoles.Add(projectRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectRole", new { id = projectRole.Id }, projectRole);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectRole(int id)
        {
            var projectRole = await _context.ProjectRoles.FindAsync(id);
            if (projectRole == null)
            {
                return NotFound();
            }

            _context.ProjectRoles.Remove(projectRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectRoleExists(int id)
        {
            return _context.ProjectRoles.Any(e => e.Id == id);
        }
    }
}
