using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateCRM.Application.DTOs.Lead;
using RealEstateCRM.Application.Features.Leads.Commands.CreateLead;
using RealEstateCRM.Application.Features.Leads.Commands.DeleteLead;
using RealEstateCRM.Application.Features.Leads.Commands.UpdateLead;
using RealEstateCRM.Application.Features.Leads.Queries.GetAllLeads;
using RealEstateCRM.Application.Features.Leads.Queries.GetLeadById;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealEstateCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeadsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeadsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? statusId, [FromQuery] int? userId)
        {
            var leads = await _mediator.Send(new GetAllLeadsQuery { StatusId = statusId, UserId = userId });
            return Ok(leads);
        }
        [HttpGet("my-leads")]
        public async Task<IActionResult> GetMyLeads([FromQuery] int? statusId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var leads = await _mediator.Send(new GetAllLeadsQuery { StatusId = statusId, UserId = userId });
            return Ok(leads);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lead = await _mediator.Send(new GetLeadByIdQuery(id));

            if (lead == null)
            {
                return NotFound(new { message = "Lead bulunamadı" });
            }

            return Ok(lead);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLeadDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            dto.CreatedByUserId = userId;

            var leadId = await _mediator.Send(new CreateLeadCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = leadId }, new { id = leadId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLeadDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest(new { message = "ID uyuşmazlığı" });
            }

            var result = await _mediator.Send(new UpdateLeadCommand(dto));

            if (!result)
            {
                return NotFound(new { message = "Lead bulunamadı" });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteLeadCommand(id));

            if (!result)
                return NotFound();

            return NoContent();
        }


    }
}