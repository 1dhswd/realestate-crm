using RealEstateCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateCRM.Domain.Entities
{
    public class PropertyType:BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Property> Properties { get; set; }

        public PropertyType()
        {
            Properties = new HashSet<Property>();
            IsActive = true;
        }
    }
}
