
namespace ProjectManagement.Domain.Entities.Base
{
    public class BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
