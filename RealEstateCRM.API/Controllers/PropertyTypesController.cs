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

    public class PropertyTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PropertyTypesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyType>>> GetAll()
        {
            var types = await _context.PropertyTypes
                .Where(t => t.IsActive)
                .OrderBy(t => t.Name)
                .ToListAsync();

            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyType>> GetById(int id)
        {
            var type = await _context.PropertyTypes.FindAsync(id);

            if (type == null)
                return NotFound(new { message = "Type not found" });

            return Ok(type);
        }

        [HttpPost]
        public async Task<ActionResult<PropertyType>> Create([FromBody] PropertyType type)
        {
            if (string.IsNullOrWhiteSpace(type.Name))
                return BadRequest(new { message = "Type name is required" });

            var exists = await _context.PropertyTypes
                .AnyAsync(t => t.Name.ToLower() == type.Name.ToLower());

            if (exists)
                return BadRequest(new { message = "Type already exists" });

            type.IsActive = true;
            _context.PropertyTypes.Add(type);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = type.Id }, type);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PropertyType type)
        {
            if (id != type.Id)
                return BadRequest(new { message = "ID mismatch" });

            var existing = await _context.PropertyTypes.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = "Type not found" });

            existing.Name = type.Name;
            existing.IsActive = type.IsActive;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var type = await _context.PropertyTypes.FindAsync(id);
            if (type == null)
                return NotFound(new { message = "Type not found" });

            var hasProperties = await _context.Properties.AnyAsync(p => p.TypeId == id);
            if (hasProperties)
                return BadRequest(new { message = "Cannot delete type that is in use" });

            _context.PropertyTypes.Remove(type);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Type deleted successfully" });
        }
    }
}