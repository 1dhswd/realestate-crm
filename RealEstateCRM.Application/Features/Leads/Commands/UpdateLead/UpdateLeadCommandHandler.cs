using AutoMapper;
using MediatR;
using RealEstateCRM.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Leads.Commands.UpdateLead
{
    public class UpdateLeadCommandHandler : IRequestHandler<UpdateLeadCommand, bool>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IMapper _mapper;

        public UpdateLeadCommandHandler(ILeadRepository leadRepository, IMapper mapper)
        {
            _leadRepository = leadRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateLeadCommand request, CancellationToken cancellationToken)
        {
            var existingLead = await _leadRepository.GetByIdAsync(request.Dto.Id);

            if (existingLead == null)
            {
                return false;
            }

            _mapper.Map(request.Dto, existingLead);
            existingLead.UpdatedAt = DateTime.UtcNow;

            await _leadRepository.UpdateAsync(existingLead);
            return true;
        }
    }
}