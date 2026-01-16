using MediatR;
using RealEstateCRM.Application.DTOs.Customer;

namespace RealEstateCRM.Application.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<CustomerDto>
    {
        public int Id { get; set; }

        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }
    }
}