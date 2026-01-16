using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateCRM.Domain.Enums
{
    public enum LeadStatus
    {
        New = 0,
        InProgress = 1,
        Contacted = 2,
        Qualified = 3,
        Converted = 4,
        Lost = 5,
        Closed = 6
    }
}
