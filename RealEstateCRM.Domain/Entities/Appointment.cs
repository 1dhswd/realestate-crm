using RealEstateCRM.Domain.Common;
using RealEstateCRM.Domain.Enums;
using System;

namespace RealEstateCRM.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public int LeadId { get; set; }
        public int PropertyId { get; set; }
        public int AgentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int Duration { get; set; } 
        public string Location { get; set; }
        public string Notes { get; set; }
        public AppointmentStatus Status { get; set; }

        public Lead Lead { get; set; }
        public Property Property { get; set; }
        public User Agent { get; set; }

        public Appointment()
        {
            Duration = 60;
            Status = AppointmentStatus.Scheduled;
        }
    }
}