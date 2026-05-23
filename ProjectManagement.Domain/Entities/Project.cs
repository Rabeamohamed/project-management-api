
using ProjectManagement.Domain.Entities.Base;

namespace ProjectManagement.Domain.Entities
{
    public class Project : BaseAuditableEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();  
    }
}

