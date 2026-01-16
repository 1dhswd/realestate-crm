using RealEstateCRM.Domain.Common;
using System.Collections.Generic;

namespace RealEstateCRM.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string Source { get; set; }
        public int StatusId { get; set; }
        public CustomerStatus Status { get; set; }


        public int? AssignedAgentId { get; set; }

        public User AssignedAgent { get; set; }
        public ICollection<Lead> Leads { get; set; }

        public Customer()
        {
            Leads = new HashSet<Lead>();
        }
    }
}