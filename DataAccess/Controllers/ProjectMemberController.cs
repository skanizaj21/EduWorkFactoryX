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
    public class ProjectMemberController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectMemberController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectMember>>> GetProjectMembers()
        {
            return await _context.ProjectMembers
                .Include(pm => pm.User)
                .Include(pm => pm.Project)
                .Include(pm => pm.ProjectRole)
                .ToListAsync();
        }

        [HttpGet("{userId}/{projectId}/{projectRoleId}")]
        public async Task<ActionResult<ProjectMember>> GetProjectMember(int userId, int projectId, int projectRoleId)
        {
            var projectMember = await _context.ProjectMembers
                .Include(pm => pm.User)
                .Include(pm => pm.Project)
                .Include(pm => pm.ProjectRole)
                .FirstOrDefaultAsync(pm =>
                    pm.UserId == userId &&
                    pm.ProjectId == projectId &&
                    pm.ProjectRoleId == projectRoleId);

            if (projectMember == null)
            {
                return NotFound();
            }

            return projectMember;
        }

        [HttpPut("{userId}/{projectId}/{projectRoleId}")]
        public async Task<IActionResult> PutProjectMember(int userId, int projectId, int projectRoleId, ProjectMember projectMember)
        {
            if (userId != projectMember.UserId || projectId != projectMember.ProjectId || projectRoleId != projectMember.ProjectRoleId)
            {
                return BadRequest();
            }

            _context.Entry(projectMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectMemberExists(userId, projectId, projectRoleId))
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
        public async Task<ActionResult<ProjectMember>> PostProjectMember(ProjectMember projectMember)
        {
            _context.ProjectMembers.Add(projectMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectMember", new { userId = projectMember.UserId, projectId = projectMember.ProjectId, projectRoleId = projectMember.ProjectRoleId }, projectMember);
        }

        [HttpDelete("{userId}/{projectId}/{projectRoleId}")]
        public async Task<IActionResult> DeleteProjectMember(int userId, int projectId, int projectRoleId)
        {
            var projectMember = await _context.ProjectMembers.FindAsync(userId, projectId, projectRoleId);
            if (projectMember == null)
            {
                return NotFound();
            }

            _context.ProjectMembers.Remove(projectMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectMemberExists(int userId, int projectId, int projectRoleId)
        {
            return _context.ProjectMembers.Any(pm =>
                pm.UserId == userId &&
                pm.ProjectId == projectId &&
                pm.ProjectRoleId == projectRoleId);
        }
    }
}
