using MediatR;
using RealEstateCRM.Application.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Appointments
{
    public class GetAppointmentsQuery : IRequest<List<AppointmentDto>>
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
