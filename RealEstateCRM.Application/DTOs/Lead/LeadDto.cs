using System;

namespace RealEstateCRM.Application.DTOs.Lead
{
    public class LeadDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? PropertyId { get; set; }
        public string PropertyTitle { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusColor { get; set; }
        public decimal? Budget { get; set; }
        public string Notes { get; set; }
        public DateTime? NextFollowUpDate { get; set; }
        public int CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}