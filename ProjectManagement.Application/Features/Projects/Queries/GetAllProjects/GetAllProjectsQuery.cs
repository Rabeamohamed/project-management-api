
using MediatR;
using ProjectManagement.Application.Features.Projects.DTOs;

namespace ProjectManagement.Application.Features.Projects.Queries.GetAllProjects
{
    public record GetAllProjectsQuery: IRequest<List<ProjectResponseDto>>;
}
