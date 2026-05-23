
using MediatR;
using ProjectManagement.Application.Features.Projects.DTOs;

namespace ProjectManagement.Application.Features.Projects.Queries.GetAllProjects
{
    public record GetAllProjectsQuery(int PageNumber = 1,int PageSize = 10,string? Search = null): IRequest<List<ProjectResponseDto>>;
}
