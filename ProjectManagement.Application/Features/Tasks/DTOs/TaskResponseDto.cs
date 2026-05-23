using ProjectManagement.Domain.Enums;

public class TaskResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public DateTime? DueDate { get; set; }
    public ProjectTaskPriority Priority { get; set; }
    public Guid ProjectId { get; set; }
}