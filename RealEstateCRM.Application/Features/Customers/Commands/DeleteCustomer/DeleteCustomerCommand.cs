using MediatR;

namespace RealEstateCRM.Application.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteCustomerCommand(int id)
        {
            Id = id;
        }
    }
}