using MediatR;
using RealEstateCRM.Application.DTOs.Dashboard;

namespace RealEstateCRM.Application.Features.Dashboard.Queries.GetDashboardStats
{
    public class GetDashboardStatsQuery : IRequest<DashboardStatsDto>
    {
        public int? UserId { get; set; }
    }
}