using MediatR;
using RealEstateCRM.Application.Interfaces.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using RealEstateCRM.Application.DTOs.Dashboard.Charts;

public class GetAppointmentMonthlyChartQueryHandler
    : IRequestHandler<GetAppointmentMonthlyChartQuery, List<AppointmentMonthlyChartDto>>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAppointmentMonthlyChartQueryHandler(
        IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<List<AppointmentMonthlyChartDto>> Handle(
        GetAppointmentMonthlyChartQuery request,
        CancellationToken cancellationToken)
    {
        var appointments = await _appointmentRepository.GetAllAsync();

        if (request.AgentId.HasValue)
        {
            appointments = appointments
                .Where(a => a.AgentId == request.AgentId.Value);
        }

        return appointments
            .GroupBy(a => new
            {
                a.AppointmentDate.Year,
                a.AppointmentDate.Month
            })
            .Select(g => new AppointmentMonthlyChartDto
            {
                Month = $"{g.Key.Month}/{g.Key.Year}",
                Count = g.Count()
            })
            .OrderBy(x => x.Month)
            .ToList();
    }
}
