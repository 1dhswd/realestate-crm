using RealEstateCRM.Domain.Common;

namespace RealEstateCRM.Domain.Entities
{
    public class ActivityLog : BaseEntity
    {
        public int UserId { get; set; }
        public string EntityType { get; set; } 
        public int EntityId { get; set; }
        public string Action { get; set; } 
        public string Description { get; set; }

        public User User { get; set; }
    }
}