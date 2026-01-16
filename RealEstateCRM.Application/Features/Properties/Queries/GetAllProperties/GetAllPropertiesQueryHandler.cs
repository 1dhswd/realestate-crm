using AutoMapper;
using MediatR;
using RealEstateCRM.Application.DTOs.Property;
using RealEstateCRM.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, List<PropertyDto>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetAllPropertiesQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<List<PropertyDto>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await _propertyRepository.GetAllWithDetailsAsync();

            if (!string.IsNullOrEmpty(request.City))
            {
                properties = properties.Where(p => p.City.ToLower().Contains(request.City.ToLower()));
            }

            if (request.CategoryId.HasValue)
            {
                properties = properties.Where(p => p.CategoryId == request.CategoryId.Value);
            }

            if (request.MinPrice.HasValue)
            {
                properties = properties.Where(p => p.Price >= request.MinPrice.Value);
            }

            if (request.MaxPrice.HasValue)
            {
                properties = properties.Where(p => p.Price <= request.MaxPrice.Value);
            }

            if (request.OnlyActive.HasValue && request.OnlyActive.Value)
            {
                properties = properties.Where(p => p.IsActive);
            }

            return _mapper.Map<List<PropertyDto>>(properties.ToList());
        }
    }
}