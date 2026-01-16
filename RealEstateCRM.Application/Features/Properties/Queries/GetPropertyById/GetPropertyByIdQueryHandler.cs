using AutoMapper;
using MediatR;
using RealEstateCRM.Application.DTOs.Property;
using RealEstateCRM.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Properties.Queries.GetPropertyById
{
    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, PropertyDto>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetPropertyByIdQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<PropertyDto> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetPropertyWithDetailsAsync(request.Id);

            if (property != null)
            {
                await _propertyRepository.IncrementViewCountAsync(request.Id);
            }

            return _mapper.Map<PropertyDto>(property);
        }
    }
}