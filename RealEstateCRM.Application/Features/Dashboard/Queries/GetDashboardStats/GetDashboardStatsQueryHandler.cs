using MediatR;
using RealEstateCRM.Application.DTOs.Dashboard;
using RealEstateCRM.Application.Interfaces.Repositories;
using RealEstateCRM.Domain.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Dashboard.Queries.GetDashboardStats
{
    public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILeadRepository _leadRepository;
        private readonly IAppointmentRepository _appointmentRepository;
     
        public GetDashboardStatsQueryHandler(
            IPropertyRepository propertyRepository,
            ICustomerRepository customerRepository,
            IAppointmentRepository appointmentRepository,
            ILeadRepository leadRepository)
        {
            _propertyRepository = propertyRepository;
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
            _leadRepository = leadRepository;
        }

        public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            var properties = await _propertyRepository.GetAllAsync();
            var customers = await _customerRepository.GetAllAsync();
            var leads = await _leadRepository.GetAllAsync();

            if (request.UserId.HasValue)
            {
                properties = properties.Where(p => p.OwnerId == request.UserId.Value);
                customers = customers.Where(c => c.AssignedAgentId == request.UserId.Value);
                leads = leads.Where(l => l.CreatedByUserId == request.UserId.Value);
            }

            var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            var today = DateTime.UtcNow.Date;

            var activeLeadStatuses = new[] { "New", "InProgress", "Contacted", "Qualified" };

            return new DashboardStatsDto
            {
                TotalProperties = properties.Count(),
                ActiveProperties = properties.Count(p => p.IsActive),
                TotalCustomers = customers.Count(),
                TotalLeads = leads.Count(),
                ActiveLeads = leads.Count(l => l.Status != null && activeLeadStatuses.Contains(l.Status.ToString())),
                TodayAppointments = await _appointmentRepository.CountTodayAsync(),
                ThisMonthAppointments = await _appointmentRepository.CountThisMonthAsync(),
                PendingOffers = 0,
                TotalPropertyValue = properties.Sum(p => p.Price),
                NewCustomersThisMonth = customers.Count(c => c.CreatedAt >= startOfMonth)
            };
        }
    }
}