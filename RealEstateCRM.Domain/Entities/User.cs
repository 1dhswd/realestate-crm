using RealEstateCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateCRM.Domain.Entities
{
    public class User:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public DateTime? LastLoginAt { get; set; }


        public ICollection<UserRole>UserRoles { get; set; }
        public ICollection<Property> Properties { get; set; }

        public ICollection<Lead> CreatedLeads { get; set; }

        public ICollection<Customer> AssignedCustomers { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        public ICollection<Offer> CreatedOffers { get; set; }
        public ICollection<ActivityLog> ActivityLogs { get; set; }


        public User()
        {
            UserRoles = new HashSet<UserRole>();
            Properties = new HashSet<Property>();
            CreatedLeads = new HashSet<Lead>();
            AssignedCustomers = new HashSet<Customer>();
            Appointments = new HashSet<Appointment>();
            CreatedOffers = new HashSet<Offer>();
            ActivityLogs = new HashSet<ActivityLog>();
            IsActive = true;
        }



    }
}
