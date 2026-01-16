using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateCRM.Domain.Entities;
using RealEstateCRM.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserRolesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult> GetUserRoles(int userId)
        {
            var userRoles = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Role)
                .Select(ur => new
                {
                    roleId = ur.RoleId,
                    roleName = ur.Role.Name,
                    roleDescription = ur.Role.Description,
                    assignedAt = ur.AssignedAt
                })
                .ToListAsync();

            return Ok(userRoles);
        }

        [HttpPost("assign")]
        public async Task<ActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            var role = await _context.Roles.FindAsync(request.RoleId);
            if (role == null)
                return NotFound(new { message = "Role not found" });

            var exists = await _context.UserRoles
                .AnyAsync(ur => ur.UserId == request.UserId && ur.RoleId == request.RoleId);

            if (exists)
                return BadRequest(new { message = "Role already assigned to user" });

            var userRole = new UserRole
            {
                UserId = request.UserId,
                RoleId = request.RoleId,
                AssignedAt = DateTime.UtcNow
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Role assigned successfully" });
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveRole([FromBody] AssignRoleRequest request)
        {
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == request.UserId && ur.RoleId == request.RoleId);

            if (userRole == null)
                return NotFound(new { message = "User role not found" });

            var role = await _context.Roles.FindAsync(request.RoleId);
            if (role?.Name == "Admin")
            {
                var adminCount = await _context.UserRoles
                    .CountAsync(ur => ur.RoleId == request.RoleId);

                if (adminCount <= 1)
                    return BadRequest(new { message = "Cannot remove the last admin role" });
            }

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Role removed successfully" });
        }

        [HttpPost("sync")]
        public async Task<ActionResult> SyncUserRoles([FromBody] SyncRolesRequest request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            var existingRoles = await _context.UserRoles
                .Where(ur => ur.UserId == request.UserId)
                .ToListAsync();

            _context.UserRoles.RemoveRange(existingRoles);

            foreach (var roleId in request.RoleIds)
            {
                var roleExists = await _context.Roles.AnyAsync(r => r.Id == roleId);
                if (!roleExists)
                    continue;

                _context.UserRoles.Add(new UserRole
                {
                    UserId = request.UserId,
                    RoleId = roleId,
                    AssignedAt = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "User roles synchronized successfully" });
        }
    }

    public class AssignRoleRequest
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

    public class SyncRolesRequest
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; }
    }
}