using MediatR;
using RealEstateCRM.Application.DTOs.User;

namespace RealEstateCRM.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}