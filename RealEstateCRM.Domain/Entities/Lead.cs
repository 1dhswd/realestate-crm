using RealEstateCRM.Domain.Common;
using System;
using System.Collections.Generic;

namespace RealEstateCRM.Domain.Entities
{
    public class Lead : BaseEntity
    {
        public int CustomerId { get; set; }
        public int? PropertyId { get; set; }
        public int StatusId { get; set; }
        public decimal? Budget { get; set; }
        public string Notes { get; set; }
        public DateTime? NextFollowUpDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime? ClosedAt { get; set; }

        public Customer Customer { get; set; }
        public Property Property { get; set; }
        public CustomerStatus Status { get; set; }
        public User CreatedByUser { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Offer> Offers { get; set; }

        public bool IsDeleted { get; set; }

        public Lead()
        {
            Appointments = new HashSet<Appointment>();
            Offers = new HashSet<Offer>();
        }
    }
}