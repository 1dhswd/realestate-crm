using AutoMapper;
using MediatR;
using RealEstateCRM.Application.Interfaces.Repositories;
using RealEstateCRM.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, int>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = _mapper.Map<Property>(request.Dto);
            property.CreatedAt = DateTime.UtcNow;
            property.PublishedAt = DateTime.UtcNow;

            var createdProperty = await _propertyRepository.AddAsync(property);

            if (request.Dto.FeatureIds != null && request.Dto.FeatureIds.Any())
            {
                property.PropertyFeatures = request.Dto.FeatureIds.Select(featureId => new PropertyPropertyFeature
                {
                    PropertyId = createdProperty.Id,
                    FeatureId = featureId
                }).ToList();
            }

            if (request.Dto.ImageUrls != null && request.Dto.ImageUrls.Any())
            {
                property.Images = request.Dto.ImageUrls.Select((url, index) => new PropertyImage
                {
                    PropertyId = createdProperty.Id,
                    ImageUrl = url,
                    DisplayOrder = index,
                    IsMainImage = index == 0,
                    UploadedAt = DateTime.UtcNow
                }).ToList();
            }

            await _propertyRepository.UpdateAsync(property);

            return createdProperty.Id;
        }
    }
}