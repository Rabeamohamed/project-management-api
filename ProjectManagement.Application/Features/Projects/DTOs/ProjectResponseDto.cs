

namespace ProjectManagement.Application.Features.Projects.DTOs
{
    public class ProjectResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
