
using ProjectManagement.Domain.Entities.Base;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Domain.Entities
{
    public class ProjectTask: BaseAuditableEntity
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public ProjectTaskStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public ProjectTaskPriority Priority { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = default!;
    }
}
