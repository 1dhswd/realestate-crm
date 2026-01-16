using RealEstateCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateCRM.Domain.Entities
{
    public class Role:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<UserRole>UserRoles { get; set; }

        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
    }
}
