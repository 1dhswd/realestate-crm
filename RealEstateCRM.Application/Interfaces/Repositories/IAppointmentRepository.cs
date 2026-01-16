using RealEstateCRM.Application.Interfaces.Repositories;
using RealEstateCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetAppointmentsWithDetailsAsync();
    Task<IEnumerable<Appointment>> GetAppointmentsByAgentAsync(int agentId);
    Task<IEnumerable<Appointment>> GetAppointmentsByDateRange(DateTime start, DateTime end);

    Task<int> CountTodayAsync();
    Task<int> CountThisMonthAsync();
}

