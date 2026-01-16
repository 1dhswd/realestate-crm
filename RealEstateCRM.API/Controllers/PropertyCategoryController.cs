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

    public class PropertyCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PropertyCategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyCategory>>> GetAll()
        {
            var categories = await _context.PropertyCategories
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyCategory>> GetById(int id)
        {
            var category = await _context.PropertyCategories.FindAsync(id);

            if (category == null)
                return NotFound(new { message = "Category not found" });

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<PropertyCategory>> Create([FromBody] PropertyCategory category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                return BadRequest(new { message = "Category name is required" });

            var exists = await _context.PropertyCategories
                .AnyAsync(c => c.Name.ToLower() == category.Name.ToLower());

            if (exists)
                return BadRequest(new { message = "Category already exists" });

            category.IsActive = true;
            _context.PropertyCategories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PropertyCategory category)
        {
            if (id != category.Id)
                return BadRequest(new { message = "ID mismatch" });

            var existing = await _context.PropertyCategories.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = "Category not found" });

            existing.Name = category.Name;
            existing.Description = category.Description;
            existing.IsActive = category.IsActive;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.PropertyCategories.FindAsync(id);
            if (category == null)
                return NotFound(new { message = "Category not found" });

            var hasProperties = await _context.Properties.AnyAsync(p => p.CategoryId == id);
            if (hasProperties)
                return BadRequest(new { message = "Cannot delete category that is in use" });

            _context.PropertyCategories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Category deleted successfully" });
        }
    }
}