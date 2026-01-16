using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateCRM.Application.Interfaces.Repositories;
using RealEstateCRM.Application.Interfaces.Services;
using RealEstateCRM.Domain.Entities;
using RealEstateCRM.Domain.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AppointmentsController : ControllerBase
    {
        private readonly IGenericRepository<Appointment> _appointmentRepository;
        private readonly IEmailService _emailService;

        public AppointmentsController(
            IGenericRepository<Appointment> appointmentRepository, 
            IEmailService emailService)
        {
            _appointmentRepository = appointmentRepository;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _appointmentRepository.GetAllAsync();

            var result = appointments.Select(a => new
            {
                a.Id,
                Title = $"Randevu - {a.PropertyId}",
                Start = a.AppointmentDate,
                End = a.AppointmentDate.AddMinutes(a.Duration),
                a.Location,
                a.Notes,
                a.Status,
                a.AgentId,
                a.PropertyId,
                a.LeadId
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var a = await _appointmentRepository.GetByIdAsync(id);

            if (a == null)
                return NotFound(new { message = "Randevu bulunamadı" });

            return Ok(new
            {
                a.Id,
                a.AppointmentDate,
                a.Duration,
                EndDate = a.AppointmentDate.AddMinutes(a.Duration),
                a.Location,
                a.Notes,
                a.Status,
                a.AgentId,
                a.PropertyId,
                a.LeadId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Appointment model)
        {
            model.Status = AppointmentStatus.Scheduled;

            await _appointmentRepository.AddAsync(model);

            return Ok(new { message = "Randevu oluşturuldu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Appointment model)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                return NotFound(new { message = "Randevu bulunamadı" });

            appointment.AppointmentDate = model.AppointmentDate;
            appointment.Duration = model.Duration;
            appointment.Location = model.Location;
            appointment.Notes = model.Notes;
            appointment.Status = model.Status;
            appointment.AgentId = model.AgentId;
            appointment.PropertyId = model.PropertyId;
            appointment.LeadId = model.LeadId;

            await _appointmentRepository.UpdateAsync(appointment);

            return Ok(new { message = "Randevu güncellendi" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                return NotFound(new { message = "Randevu bulunamadı" });

            appointment.Status = AppointmentStatus.Cancelled;

            await _appointmentRepository.UpdateAsync(appointment);

            return Ok(new { message = "Randevu iptal edildi" });
        }
    }
}
