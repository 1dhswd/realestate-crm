using AutoMapper;
using MediatR;
using RealEstateCRM.Application.DTOs.Lead;
using RealEstateCRM.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Leads.Queries.GetLeadById
{
    public class GetLeadByIdQueryHandler : IRequestHandler<GetLeadByIdQuery, LeadDto>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IMapper _mapper;

        public GetLeadByIdQueryHandler(ILeadRepository leadRepository, IMapper mapper)
        {
            _leadRepository = leadRepository;
            _mapper = mapper;
        }

        public async Task<LeadDto> Handle(GetLeadByIdQuery request, CancellationToken cancellationToken)
        {
            var lead = await _leadRepository.GetLeadWithDetailsAsync(request.Id);
            return _mapper.Map<LeadDto>(lead);
        }
    }
}