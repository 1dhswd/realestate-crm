using RealEstateCRM.Domain.Common;
using System.Collections.Generic;

namespace RealEstateCRM.Domain.Entities
{
    public class PropertyFeature : BaseEntity
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }

        // Navigation Properties
        public ICollection<PropertyPropertyFeature> PropertyPropertyFeatures { get; set; }

        public PropertyFeature()
        {
            PropertyPropertyFeatures = new HashSet<PropertyPropertyFeature>();
            IsActive = true;
        }
    }
}