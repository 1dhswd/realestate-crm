using MediatR;
using RealEstateCRM.Application.DTOs.Customer;

namespace RealEstateCRM.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public CreateCustomerDto Dto { get; set; }

        public CreateCustomerCommand(CreateCustomerDto dto)
        {
            Dto = dto;
        }
    }
}