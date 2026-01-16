using AutoMapper;
using MediatR;
using RealEstateCRM.Application.Interfaces.Repositories;
using RealEstateCRM.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request.Dto);

            customer.CreatedAt = DateTime.UtcNow;

            customer.StatusId = 1;

            var createdCustomer = await _customerRepository.AddAsync(customer);
            return createdCustomer.Id;
        }
    }
}
