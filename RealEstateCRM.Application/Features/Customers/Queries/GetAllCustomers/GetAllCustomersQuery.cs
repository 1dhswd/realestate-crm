using MediatR;
using RealEstateCRM.Application.DTOs.Customer;
using System.Collections.Generic;

namespace RealEstateCRM.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<List<CustomerDto>>
    {
        public int? AssignedAgentId { get; set; }
    }
}