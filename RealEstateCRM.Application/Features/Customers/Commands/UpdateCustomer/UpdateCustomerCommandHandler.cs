using MediatR;
using RealEstateCRM.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Dto.Id);
            if (customer == null)
                return false;

            customer.FirstName = request.Dto.FirstName;
            customer.LastName = request.Dto.LastName;
            customer.Email = request.Dto.Email;
            customer.PhoneNumber = request.Dto.PhoneNumber;
            customer.Address = request.Dto.Address;
            customer.Notes = request.Dto.Notes;
            customer.Source = request.Dto.Source;

            if (request.Dto.StatusId.HasValue)
                customer.StatusId = request.Dto.StatusId.Value;

            if (request.Dto.AssignedAgentId.HasValue)
                customer.AssignedAgentId = request.Dto.AssignedAgentId.Value;

            await _customerRepository.UpdateAsync(customer);
            return true;
        }
    }
}