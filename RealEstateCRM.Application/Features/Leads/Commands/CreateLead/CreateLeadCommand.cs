using MediatR;
using RealEstateCRM.Application.DTOs.Lead;

namespace RealEstateCRM.Application.Features.Leads.Commands.CreateLead
{
    public class CreateLeadCommand : IRequest<int>
    {
        public CreateLeadDto Dto { get; set; }

        public CreateLeadCommand(CreateLeadDto dto)
        {
            Dto = dto;
        }
    }
}