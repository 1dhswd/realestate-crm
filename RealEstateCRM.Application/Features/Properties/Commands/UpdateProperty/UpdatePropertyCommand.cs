using MediatR;
using RealEstateCRM.Application.DTOs.Property;

namespace RealEstateCRM.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommand : IRequest<bool>
    {
        public UpdatePropertyDto Dto { get; set; }

        public UpdatePropertyCommand(UpdatePropertyDto dto)
        {
            Dto = dto;
        }
    }
}