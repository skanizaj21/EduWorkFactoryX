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
    public class ProjectCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectCategory>>> GetProjectCategories()
        {
            return await _context.ProjectCategories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectCategory>> GetProjectCategory(int id)
        {
            var projectCategory = await _context.ProjectCategories.FindAsync(id);

            if (projectCategory == null)
            {
                return NotFound();
            }

            return projectCategory;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectCategory(int id, ProjectCategory projectCategory)
        {
            if (id != projectCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectCategoryExists(id))
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
        public async Task<ActionResult<ProjectCategory>> PostProjectCategory(ProjectCategory projectCategory)
        {
            _context.ProjectCategories.Add(projectCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectCategory", new { id = projectCategory.Id }, projectCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectCategory(int id)
        {
            var projectCategory = await _context.ProjectCategories.FindAsync(id);
            if (projectCategory == null)
            {
                return NotFound();
            }

            _context.ProjectCategories.Remove(projectCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectCategoryExists(int id)
        {
            return _context.ProjectCategories.Any(e => e.Id == id);
        }
    }
}
