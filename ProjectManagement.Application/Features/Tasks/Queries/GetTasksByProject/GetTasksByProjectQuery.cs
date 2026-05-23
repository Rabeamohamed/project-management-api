using MediatR;
using ProjectManagement.Application.Features.Tasks.DTOs;

namespace ProjectManagement.Application.Features.Tasks.Queries.GetTasksByProject;

public record GetTasksByProjectQuery(Guid ProjectId): IRequest<List<TaskResponseDto>>;