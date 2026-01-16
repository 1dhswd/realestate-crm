using System;

namespace RealEstateCRM.Application.DTOs.Lead
{
    public class CreateLeadDto
    {
        public int CustomerId { get; set; }
        public int? PropertyId { get; set; }
        public int StatusId { get; set; }
        public decimal? Budget { get; set; }
        public string Notes { get; set; }
        public DateTime? NextFollowUpDate { get; set; }
        public int CreatedByUserId { get; set; }
    }
}