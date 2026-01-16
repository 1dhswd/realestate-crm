using AutoMapper;
using MediatR;
using RealEstateCRM.Application.DTOs.Customer;
using RealEstateCRM.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = request.AssignedAgentId.HasValue
            ? await _customerRepository.GetCustomersByAgentAsync(request.AssignedAgentId.Value)
            : await _customerRepository.GetAllWithStatusAsync();


            return _mapper.Map<List<CustomerDto>>(customers.ToList());
        }
    }
}