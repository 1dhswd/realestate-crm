using AutoMapper;
using MediatR;
using RealEstateCRM.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, bool>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public UpdatePropertyCommandHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var existingProperty = await _propertyRepository.GetByIdAsync(request.Dto.Id);

            if (existingProperty == null)
            {
                return false;
            }

            _mapper.Map(request.Dto, existingProperty);
            existingProperty.UpdatedAt = DateTime.UtcNow;

            await _propertyRepository.UpdateAsync(existingProperty);

            return true;
        }
    }
}