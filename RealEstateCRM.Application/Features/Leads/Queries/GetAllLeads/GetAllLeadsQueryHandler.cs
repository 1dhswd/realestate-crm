using AutoMapper;
using MediatR;
using RealEstateCRM.Application.DTOs.Lead;
using RealEstateCRM.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Leads.Queries.GetAllLeads
{
    public class GetAllLeadsQueryHandler : IRequestHandler<GetAllLeadsQuery, List<LeadDto>>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IMapper _mapper;

        public GetAllLeadsQueryHandler(ILeadRepository leadRepository, IMapper mapper)
        {
            _leadRepository = leadRepository;
            _mapper = mapper;
        }

        public async Task<List<LeadDto>> Handle(GetAllLeadsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.Lead> leads;

            if (request.StatusId.HasValue)
            {
                leads = await _leadRepository.GetLeadsByStatusAsync(request.StatusId.Value);
            }
            else if (request.UserId.HasValue)
            {
                leads = await _leadRepository.GetLeadsByUserAsync(request.UserId.Value);
            }
            else
            {
                leads = await _leadRepository.GetAllWithDetailsAsync();
            }

            return _mapper.Map<List<LeadDto>>(leads.ToList());
        }
    }
}