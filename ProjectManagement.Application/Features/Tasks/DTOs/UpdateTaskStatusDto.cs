using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Application.Features.Tasks.DTOs;

public class UpdateTaskStatusDto
{
    public ProjectTaskStatus Status { get; set; }
}