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

    public class PropertyFeaturesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PropertyFeaturesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyFeature>>> GetAll()
        {
            var features = await _context.PropertyFeatures
                .Where(f => f.IsActive)
                .OrderBy(f => f.Name)
                .ToListAsync();

            return Ok(features);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyFeature>> GetById(int id)
        {
            var feature = await _context.PropertyFeatures.FindAsync(id);

            if (feature == null)
                return NotFound(new { message = "Feature not found" });

            return Ok(feature);
        }

        [HttpPost]
        public async Task<ActionResult<PropertyFeature>> Create([FromBody] PropertyFeature feature)
        {
            if (string.IsNullOrWhiteSpace(feature.Name))
                return BadRequest(new { message = "Feature name is required" });

            var exists = await _context.PropertyFeatures
                .AnyAsync(f => f.Name.ToLower() == feature.Name.ToLower());

            if (exists)
                return BadRequest(new { message = "Feature already exists" });

            feature.IsActive = true;
            _context.PropertyFeatures.Add(feature);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = feature.Id }, feature);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PropertyFeature feature)
        {
            if (id != feature.Id)
                return BadRequest(new { message = "ID mismatch" });

            var existing = await _context.PropertyFeatures.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = "Feature not found" });

            existing.Name = feature.Name;
            existing.Icon = feature.Icon;
            existing.IsActive = feature.IsActive;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var feature = await _context.PropertyFeatures.FindAsync(id);
            if (feature == null)
                return NotFound(new { message = "Feature not found" });

            var hasProperties = await _context.PropertyPropertyFeatures
                .AnyAsync(pf => pf.FeatureId == id);

            if (hasProperties)
                return BadRequest(new { message = "Cannot delete feature that is in use" });

            _context.PropertyFeatures.Remove(feature);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Feature deleted successfully" });
        }
    }
}