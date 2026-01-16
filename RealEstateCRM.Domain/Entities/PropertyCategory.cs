using RealEstateCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateCRM.Domain.Entities
{
    public class PropertyCategory: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Property> Properties { get; set; }

        public PropertyCategory()
        {
            Properties = new HashSet<Property>();
            IsActive = true;
        }
    }
}
