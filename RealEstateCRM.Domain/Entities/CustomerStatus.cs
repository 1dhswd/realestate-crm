using RealEstateCRM.Domain.Common;
using System.Collections.Generic;

namespace RealEstateCRM.Domain.Entities
{
    public class CustomerStatus : BaseEntity
    {
        public string Name { get; set; }
        public string ColorCode { get; set; }
        public int DisplayOrder { get; set; }

        public ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
    }
}
