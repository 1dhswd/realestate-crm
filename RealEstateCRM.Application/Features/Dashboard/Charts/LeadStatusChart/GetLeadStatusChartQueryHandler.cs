using MediatR;
using RealEstateCRM.Application.DTOs.Dashboard.Charts;
using RealEstateCRM.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Dashboard.Charts.LeadStatusChart
{
    public class GetLeadStatusChartQueryHandler
        : IRequestHandler<GetLeadStatusChartQuery, List<LeadStatusChartDto>>
    {
        private readonly ILeadRepository _leadRepository;

        public GetLeadStatusChartQueryHandler(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        public async Task<List<LeadStatusChartDto>> Handle(
     GetLeadStatusChartQuery request,
     CancellationToken cancellationToken)
        {
            var leads = await _leadRepository.GetAllAsync();

            if (request.UserId.HasValue)
            {
                leads = leads.Where(l => l.CreatedByUserId == request.UserId.Value);
            }

            return leads
                .GroupBy(l => l.Status)
                .Select(g => new LeadStatusChartDto
                {
                    Status = g.Key?.ToString() ?? "Unknown",
                    Count = g.Count()
                })
                .ToList();

        }

    }
}
