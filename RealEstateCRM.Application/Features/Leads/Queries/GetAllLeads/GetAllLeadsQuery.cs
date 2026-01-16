using MediatR;
using RealEstateCRM.Application.DTOs.Lead;
using System.Collections.Generic;

namespace RealEstateCRM.Application.Features.Leads.Queries.GetAllLeads
{
    public class GetAllLeadsQuery : IRequest<List<LeadDto>>
    {
        public int? StatusId { get; set; }
        public int? UserId { get; set; }
    }
}