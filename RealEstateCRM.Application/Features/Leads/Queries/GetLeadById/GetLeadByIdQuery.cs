using MediatR;
using RealEstateCRM.Application.DTOs.Lead;

namespace RealEstateCRM.Application.Features.Leads.Queries.GetLeadById
{
    public class GetLeadByIdQuery : IRequest<LeadDto>
    {
        public int Id { get; set; }

        public GetLeadByIdQuery(int id)
        {
            Id = id;
        }
    }
}