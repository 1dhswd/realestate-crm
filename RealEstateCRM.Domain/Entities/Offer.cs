using RealEstateCRM.Domain.Common;
using RealEstateCRM.Domain.Enums;
using System;

namespace RealEstateCRM.Domain.Entities
{
    public class Offer : BaseEntity
    {
        public int LeadId { get; set; }
        public int PropertyId { get; set; }
        public decimal OfferedPrice { get; set; }
        public string Message { get; set; }
        public OfferStatus Status { get; set; }
        public DateTime ValidUntil { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime? RespondedAt { get; set; }

        public Lead Lead { get; set; }
        public Property Property { get; set; }
        public User CreatedByUser { get; set; }

        public Offer()
        {
            Status = OfferStatus.Pending;
        }
    }
}