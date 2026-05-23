using MediatR;
namespace ProjectManagement.Application.Features.Tasks.Queries.GetTasksByProject;
public record GetTasksByProjectQuery( Guid ProjectId,int PageNumber = 1,int PageSize = 10,
    string? Search = null, int? Status = null,int? Priority = null): IRequest<List<TaskResponseDto>>;