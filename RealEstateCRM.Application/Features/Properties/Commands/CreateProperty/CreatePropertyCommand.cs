using MediatR;
using RealEstateCRM.Application.DTOs.Property;

namespace RealEstateCRM.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommand : IRequest<int>
    {
        public CreatePropertyDto Dto { get; set; }

        public CreatePropertyCommand(CreatePropertyDto dto)
        {
            Dto = dto;
        }
    }
}