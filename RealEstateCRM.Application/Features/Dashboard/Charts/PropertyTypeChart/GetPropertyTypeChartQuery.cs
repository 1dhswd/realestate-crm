using MediatR;
using RealEstateCRM.Application.DTOs.Dashboard.Charts;
using System;
using System.Collections.Generic;

namespace RealEstateCRM.Application.Features.Dashboard.Charts.PropertyTypeChart
{
    public class GetPropertyTypeChartQuery
     : IRequest<List<PropertyTypeChartDto>>
    {
        public int? OwnerId { get; set; }
    }

}
