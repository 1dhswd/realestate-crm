using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateCRM.Application.DTOs.Property;
using RealEstateCRM.Application.Features.Properties.Commands.CreateProperty;
using RealEstateCRM.Application.Features.Properties.Commands.DeleteProperty;
using RealEstateCRM.Application.Features.Properties.Commands.UpdateProperty;
using RealEstateCRM.Application.Features.Properties.Queries.GetAllProperties;
using RealEstateCRM.Application.Features.Properties.Queries.GetPropertyById;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealEstateCRM.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertiesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPropertiesQuery query)
        {
            var properties = await _mediator.Send(query);
            return Ok(properties);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var property = await _mediator.Send(new GetPropertyByIdQuery(id));

            if (property == null)
            {
                return NotFound(new { message = "İlan bulunamadı" });
            }

            return Ok(property);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePropertyDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            dto.OwnerId = userId;

            var propertyId = await _mediator.Send(new CreatePropertyCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = propertyId }, new { id = propertyId });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePropertyDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest(new { message = "ID uyuşmazlığı" });
            }

            var result = await _mediator.Send(new UpdatePropertyCommand(dto));

            if (!result)
            {
                return NotFound(new { message = "İlan bulunamadı" });
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeletePropertyCommand(id));

            if (!result)
            {
                return NotFound(new { message = "İlan bulunamadı" });
            }

            return NoContent();
        }
    }
}