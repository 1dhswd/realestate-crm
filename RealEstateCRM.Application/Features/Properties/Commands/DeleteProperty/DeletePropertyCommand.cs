using MediatR;

namespace RealEstateCRM.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeletePropertyCommand(int id)
        {
            Id = id;
        }
    }
}