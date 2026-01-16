using Microsoft.EntityFrameworkCore;
using RealEstateCRM.Domain.Entities;
using RealEstateCRM.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateCRM.Persistence.Repositories
{
    public class AppointmentRepository
     : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Appointment>> GetAppointmentsWithDetailsAsync()
        {
            return await _dbSet
                .Include(a => a.Lead)
                .Include(a => a.Property)
                .Include(a => a.Agent)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByAgentAsync(int agentId)
        {
            return await _dbSet
                .Where(a => a.AgentId == agentId)
                .Include(a => a.Lead)
                .Include(a => a.Property)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDateRange(
            DateTime start, DateTime end)
        {
            return await _dbSet
                .Where(a => a.AppointmentDate >= start && a.AppointmentDate <= end)
                .Include(a => a.Lead)
                .Include(a => a.Property)
                .Include(a => a.Agent)
                .ToListAsync();
        }
        public async Task<int> CountTodayAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _dbSet.CountAsync(a =>
                a.AppointmentDate.Date == today);
        }

        public async Task<int> CountThisMonthAsync()
        {
            var now = DateTime.UtcNow;
            var start = new DateTime(now.Year, now.Month, 1);

            return await _dbSet.CountAsync(a =>
                a.AppointmentDate >= start &&
                a.AppointmentDate <= now);
        }

    }

}
