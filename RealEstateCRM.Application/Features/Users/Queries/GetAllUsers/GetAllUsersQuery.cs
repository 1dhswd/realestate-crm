using MediatR;
using RealEstateCRM.Application.DTOs.User;
using System.Collections.Generic;

namespace RealEstateCRM.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
    }
}