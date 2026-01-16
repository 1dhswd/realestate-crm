using MediatR;
using RealEstateCRM.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Leads.Commands.DeleteLead
{
    public class DeleteLeadCommandHandler : IRequestHandler<DeleteLeadCommand, bool>
    {
        private readonly ILeadRepository _leadRepository;

        public DeleteLeadCommandHandler(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        public async Task<bool> Handle(DeleteLeadCommand request, CancellationToken cancellationToken)
        {
            var lead = await _leadRepository.GetByIdAsync(request.Id);
            if (lead == null)
                return false;

            lead.IsDeleted = true;

            await _leadRepository.UpdateAsync(lead);
            return true;
        }
    }
}
