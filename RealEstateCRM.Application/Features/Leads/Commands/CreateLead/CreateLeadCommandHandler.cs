using AutoMapper;
using MediatR;
using RealEstateCRM.Application.Interfaces.Repositories;
using RealEstateCRM.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Leads.Commands.CreateLead
{
    public class CreateLeadCommandHandler : IRequestHandler<CreateLeadCommand, int>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IMapper _mapper;

        public CreateLeadCommandHandler(ILeadRepository leadRepository, IMapper mapper)
        {
            _leadRepository = leadRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeadCommand request, CancellationToken cancellationToken)
        {
            var lead = _mapper.Map<Lead>(request.Dto);
            lead.CreatedAt = DateTime.UtcNow;

            var createdLead = await _leadRepository.AddAsync(lead);
            return createdLead.Id;
        }
    }
}