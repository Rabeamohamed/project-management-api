using ProjectManagement.Domain.Enums;
public class CreateTaskDto
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public ProjectTaskPriority Priority { get; set; }
    public Guid ProjectId { get; set; }
}