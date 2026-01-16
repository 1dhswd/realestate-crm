using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateCRM.Domain.Entities;
using RealEstateCRM.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RolesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetAll()
        {
            var roles = await _context.Roles
                .OrderBy(r => r.Name)
                .ToListAsync();

            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetById(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
                return NotFound(new { message = "Role not found" });

            return Ok(role);
        }

        [HttpGet("{id}/users")]
        public async Task<ActionResult> GetRoleUsers(int id)
        {
            var users = await _context.UserRoles
                .Where(ur => ur.RoleId == id)
                .Include(ur => ur.User)
                .Select(ur => new
                {
                    id = ur.User.Id,
                    firstName = ur.User.FirstName,
                    lastName = ur.User.LastName,
                    email = ur.User.Email,
                    assignedAt = ur.AssignedAt
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<Role>> Create([FromBody] Role role)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
                return BadRequest(new { message = "Role name is required" });

            var exists = await _context.Roles
                .AnyAsync(r => r.Name.ToLower() == role.Name.ToLower());

            if (exists)
                return BadRequest(new { message = "Role already exists" });

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Role role)
        {
            if (id != role.Id)
                return BadRequest(new { message = "ID mismatch" });

            var existing = await _context.Roles.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = "Role not found" });

            if (existing.Name == "Admin" || existing.Name == "Agent" || existing.Name == "Manager")
                return BadRequest(new { message = "Cannot modify system roles" });

            existing.Name = role.Name;
            existing.Description = role.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return NotFound(new { message = "Role not found" });

            if (role.Name == "Admin" || role.Name == "Agent" || role.Name == "Manager")
                return BadRequest(new { message = "Cannot delete system roles" });

            var hasUsers = await _context.UserRoles.AnyAsync(ur => ur.RoleId == id);
            if (hasUsers)
                return BadRequest(new { message = "Cannot delete role that is assigned to users" });

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Role deleted successfully" });
        }
    }
}