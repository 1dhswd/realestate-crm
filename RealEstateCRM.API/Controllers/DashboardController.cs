using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateCRM.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("my-stats")]
        public async Task<IActionResult> GetMyStats()
        {
            try
            {
                var stats = new
                {
                    totalProperties = await _context.Properties.CountAsync(),
                    totalCustomers = await _context.Customers.CountAsync(),
                    totalLeads = await _context.Leads.CountAsync(),
                    totalAppointments = await _context.Appointments.CountAsync(),
                    activeProperties = await _context.Properties.CountAsync(p => p.IsActive),
                    pendingLeads = await _context.Leads.CountAsync()
                };

                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error: {ex.Message}" });
            }
        }
        [HttpGet("charts/appointments-monthly")]
        public async Task<IActionResult> GetAppointmentMonthlyChart()
        {
            try
            {
                var sixMonthsAgo = DateTime.UtcNow.AddMonths(-6);

                var appointmentsData = await _context.Appointments
                    .Where(a => a.AppointmentDate >= sixMonthsAgo)
                    .ToListAsync(); 

                var appointments = appointmentsData
                    .GroupBy(a => new { a.AppointmentDate.Year, a.AppointmentDate.Month })
                    .Select(g => new
                    {
                        year = g.Key.Year,
                        monthNumber = g.Key.Month,
                        month = $"{g.Key.Month}/{g.Key.Year}",
                        count = g.Count()
                    })
                    .OrderBy(x => x.year)
                    .ThenBy(x => x.monthNumber)
                    .ToList();

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new { message = $"Error: {ex.Message}" });
            }
        }
        [HttpGet("charts/lead-status")]
        public async Task<IActionResult> GetLeadStatusChart()
        {
            try
            {
                var totalLeads = await _context.Leads.CountAsync();

                var data = new[]
                {
                    new { status = "Total Leads", count = totalLeads }
                };

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error: {ex.Message}" });
            }
        }
        [HttpGet("charts/property-type")]
        public async Task<IActionResult> GetPropertyTypeChart()
        {
            try
            {
                var propertyTypes = await _context.Properties
                    .Include(p => p.Type)
                    .GroupBy(p => p.Type.Name)
                    .Select(g => new
                    {
                        type = g.Key,
                        count = g.Count()
                    })
                    .ToListAsync();

                return Ok(propertyTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error: {ex.Message}" });
            }
        }
    }
}