using MediatR;
using RealEstateCRM.Application.DTOs.Customer;

namespace RealEstateCRM.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<bool>
    {
        public UpdateCustomerDto Dto { get; set; }

        public UpdateCustomerCommand(UpdateCustomerDto dto)
        {
            Dto = dto;
        }
    }
}