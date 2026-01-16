using MediatR;
using RealEstateCRM.Application.DTOs.Lead;

namespace RealEstateCRM.Application.Features.Leads.Commands.UpdateLead
{
    public class UpdateLeadCommand : IRequest<bool>
    {
        public UpdateLeadDto Dto { get; set; }

        public UpdateLeadCommand(UpdateLeadDto dto)
        {
            Dto = dto;
        }
    }
}