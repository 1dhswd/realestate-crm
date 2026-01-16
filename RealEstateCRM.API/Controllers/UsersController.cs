using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateCRM.Application.Features.Users.Queries.GetAllUsers;
using RealEstateCRM.Application.Features.Users.Queries.GetUserById;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealEstateCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));

            if (user == null)
            {
                return NotFound(new { message = "Kullanıcı bulunamadı" });
            }

            return Ok(user);
        }
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _mediator.Send(new GetUserByIdQuery(userId));
            return Ok(user);
        }
    }
}
