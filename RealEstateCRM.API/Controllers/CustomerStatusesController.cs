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
    public class CustomerStatusesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerStatusesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerStatus>>> GetAll()
        {
            var statuses = await _context.CustomerStatuses
                .OrderBy(s => s.DisplayOrder)
                .ToListAsync();

            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerStatus>> GetById(int id)
        {
            var status = await _context.CustomerStatuses.FindAsync(id);

            if (status == null)
                return NotFound(new { message = "Status not found" });

            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerStatus>> Create([FromBody] CustomerStatus status)
        {
            if (string.IsNullOrWhiteSpace(status.Name))
                return BadRequest(new { message = "Status name is required" });

            var exists = await _context.CustomerStatuses
                .AnyAsync(s => s.Name.ToLower() == status.Name.ToLower());

            if (exists)
                return BadRequest(new { message = "Status already exists" });

            var maxOrder = await _context.CustomerStatuses.MaxAsync(s => (int?)s.DisplayOrder) ?? 0;
            status.DisplayOrder = maxOrder + 1;

            _context.CustomerStatuses.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = status.Id }, status);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerStatus status)
        {
            if (id != status.Id)
                return BadRequest(new { message = "ID mismatch" });

            var existing = await _context.CustomerStatuses.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = "Status not found" });

            existing.Name = status.Name;
            existing.ColorCode = status.ColorCode;
            existing.DisplayOrder = status.DisplayOrder;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("reorder")]
        public async Task<IActionResult> Reorder([FromBody] List<int> statusIds)
        {
            for (int i = 0; i < statusIds.Count; i++)
            {
                var status = await _context.CustomerStatuses.FindAsync(statusIds[i]);
                if (status != null)
                {
                    status.DisplayOrder = i + 1;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Statuses reordered successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _context.CustomerStatuses.FindAsync(id);
            if (status == null)
                return NotFound(new { message = "Status not found" });

            var hasCustomers = await _context.Customers.AnyAsync(c => c.StatusId == id);
            if (hasCustomers)
                return BadRequest(new { message = "Cannot delete status that is in use" });

            _context.CustomerStatuses.Remove(status);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Status deleted successfully" });
        }
    }
}