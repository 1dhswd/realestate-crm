using AutoMapper;
using MediatR;
using RealEstateCRM.Application.DTOs.Customer;
using RealEstateCRM.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerWithLeadsAsync(request.Id);
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}