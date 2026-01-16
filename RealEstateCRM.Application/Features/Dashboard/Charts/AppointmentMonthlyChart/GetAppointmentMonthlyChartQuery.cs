using MediatR;
using RealEstateCRM.Application.DTOs.Dashboard.Charts;
using System.Collections.Generic;

public class GetAppointmentMonthlyChartQuery
    : IRequest<List<AppointmentMonthlyChartDto>>
{
    public int? AgentId { get; set; }
}
