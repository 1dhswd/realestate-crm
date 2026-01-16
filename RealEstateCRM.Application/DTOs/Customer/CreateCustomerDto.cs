using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.DTOs.Customer
{
    public class CreateCustomerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string Source { get; set; }
        public int? AssignedAgentId { get; set; }
    }
}
