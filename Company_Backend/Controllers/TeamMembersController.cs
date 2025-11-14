using Company_Backend.Data;
using Company_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeamMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamMember>>> GetAll()
        {
            return await _context.TeamMembers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamMember>> GetById(int id)
        {
            var member = await _context.TeamMembers.FindAsync(id);
            if (member == null)
                return NotFound();

            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult<TeamMember>> Create([FromForm] TeamMember member, IFormFile? photo)
        {
            if (photo != null && photo.Length > 0)
            {
                using var ms = new MemoryStream();
                await photo.CopyToAsync(ms);
                member.Photo = ms.ToArray();
            }

            _context.TeamMembers.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = member.Id }, member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] TeamMember updatedMember, IFormFile? photo)
        {
            if (id != updatedMember.Id)
                return BadRequest();

            var existingMember = await _context.TeamMembers.FindAsync(id);
            if (existingMember == null)
                return NotFound();

            existingMember.Name = updatedMember.Name;
            existingMember.Role = updatedMember.Role;
            existingMember.Bio = updatedMember.Bio;
            existingMember.FacebookUrl = updatedMember.FacebookUrl;
            existingMember.InstagramUrl = updatedMember.InstagramUrl;
            existingMember.LinkedInUrl = updatedMember.LinkedInUrl;
            existingMember.GitHubUrl = updatedMember.GitHubUrl;

            if (photo != null && photo.Length > 0)
            {
                using var ms = new MemoryStream();
                await photo.CopyToAsync(ms);
                existingMember.Photo = ms.ToArray();
            }

            _context.Entry(existingMember).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _context.TeamMembers.FindAsync(id);
            if (member == null)
                return NotFound();

            _context.TeamMembers.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
