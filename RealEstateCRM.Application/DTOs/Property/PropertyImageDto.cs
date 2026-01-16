using System;

namespace RealEstateCRM.Application.DTOs.Property
{
    public class PropertyImageDto
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string ImageUrl { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}