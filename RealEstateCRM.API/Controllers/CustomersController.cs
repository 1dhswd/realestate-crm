using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateCRM.Application.DTOs.Customer;
using RealEstateCRM.Application.Features.Customers.Commands.CreateCustomer;
using RealEstateCRM.Application.Features.Customers.Commands.UpdateCustomer;
using RealEstateCRM.Application.Features.Customers.Commands.DeleteCustomer;
using RealEstateCRM.Application.Features.Customers.Queries.GetAllCustomers;
using RealEstateCRM.Application.Features.Customers.Queries.GetCustomerById;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealEstateCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? assignedAgentId)
        {
            var customers = await _mediator.Send(new GetAllCustomersQuery { AssignedAgentId = assignedAgentId });
            return Ok(customers);
        }

        [HttpGet("my-customers")]
        public async Task<IActionResult> GetMyCustomers()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var customers = await _mediator.Send(new GetAllCustomersQuery { AssignedAgentId = userId });
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _mediator.Send(new GetCustomerByIdQuery(id));
            if (customer == null)
            {
                return NotFound(new { message = "Müşteri bulunamadı" });
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
        {
            if (!dto.AssignedAgentId.HasValue)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                dto.AssignedAgentId = userId;
            }

            var customerId = await _mediator.Send(new CreateCustomerCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = customerId }, new { id = customerId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest(new { message = "ID uyuşmazlığı" });
            }

            var result = await _mediator.Send(new UpdateCustomerCommand(dto));
            if (!result)
            {
                return NotFound(new { message = "Müşteri bulunamadı" });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));
            if (!result)
            {
                return NotFound(new { message = "Müşteri bulunamadı" });
            }

            return Ok(new { message = "Müşteri başarıyla silindi" });
        }
    }
}