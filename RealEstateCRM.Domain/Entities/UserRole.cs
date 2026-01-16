using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateCRM.Domain.Entities
{
    public class UserRole
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public DateTime AssignedAt { get; set; }

            public UserRole()
        {
            AssignedAt = DateTime.UtcNow;
        }
    }
}
