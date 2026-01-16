using MediatR;
using RealEstateCRM.Application.DTOs.Dashboard.Charts;
using System;
using System.Collections.Generic;

namespace RealEstateCRM.Application.Features.Dashboard.Charts.LeadStatusChart
{
    public class GetLeadStatusChartQuery
      : IRequest<List<LeadStatusChartDto>>
    {
        public int? UserId { get; set; }
    }

}
