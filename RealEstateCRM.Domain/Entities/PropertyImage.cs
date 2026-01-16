using RealEstateCRM.Domain.Common;
using System;

namespace RealEstateCRM.Domain.Entities
{
    public class PropertyImage : BaseEntity
    {
        public int PropertyId { get; set; }
        public string ImageUrl { get; set; }
        public string FileName { get; set; } 
        public long FileSize { get; set; } 
        public int DisplayOrder { get; set; }
        public bool IsMainImage { get; set; }
        public DateTime UploadedAt { get; set; }

        // Navigation Properties
        public virtual Property Property { get; set; }

        public PropertyImage()
        {
            UploadedAt = DateTime.UtcNow;
            IsMainImage = false;
            DisplayOrder = 0;
        }
    }
}