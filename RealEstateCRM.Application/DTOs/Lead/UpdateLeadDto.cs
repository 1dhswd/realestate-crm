using System;

namespace RealEstateCRM.Application.DTOs.Lead
{
    public class UpdateLeadDto
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public decimal? Budget { get; set; }
        public string Notes { get; set; }
        public DateTime? NextFollowUpDate { get; set; }
    }
}