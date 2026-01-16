using MediatR;
using RealEstateCRM.Application.DTOs.Dashboard.Charts;
using RealEstateCRM.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Dashboard.Charts.PropertyTypeChart
{
    public class GetPropertyTypeChartQueryHandler
        : IRequestHandler<GetPropertyTypeChartQuery, List<PropertyTypeChartDto>>
    {
        private readonly IPropertyRepository _propertyRepository;

        public GetPropertyTypeChartQueryHandler(
            IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<List<PropertyTypeChartDto>> Handle(
    GetPropertyTypeChartQuery request,
    CancellationToken cancellationToken)
        {
            var properties = await _propertyRepository.GetAllAsync();

            if (request.OwnerId.HasValue)
            {
                properties = properties
                    .Where(p => p.OwnerId == request.OwnerId.Value);
            }

            return properties
                .GroupBy(p => p.Type)
                .Select(g => new PropertyTypeChartDto
                {
                    Type = g.Key?.ToString() ?? "Unknown",
                    Count = g.Count()
                })
                .ToList();
        }

    }
}
