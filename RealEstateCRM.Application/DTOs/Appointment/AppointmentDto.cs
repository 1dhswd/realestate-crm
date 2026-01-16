using RealEstateCRM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.DTOs.Appointment
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int Duration { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public AppointmentStatus Status { get; set; }

        public string LeadName { get; set; }
        public string PropertyTitle { get; set; }
        public string AgentName { get; set; }
    }

}
