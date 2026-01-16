using System;

namespace RealEstateCRM.Application.DTOs.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string Source { get; set; }

        public string StatusName { get; set; }
        public int? AssignedAgentId { get; set; }
        public string AssignedAgentName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}