using MediatR;
using ProjectManagement.Application.Features.Tasks.DTOs;
using ProjectManagement.Domain.Enums;

public record CreateTaskCommand(string Title,string? Description,DateTime DueDate,ProjectTaskPriority Priority,Guid ProjectId): IRequest<TaskResponseDto>;