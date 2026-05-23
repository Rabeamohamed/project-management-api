

using MediatR;
using ProjectManagement.Application.Features.Projects.DTOs;

namespace ProjectManagement.Application.Features.Projects.Queries.GetProjectById
{
    public record GetProjectByIdQuery(Guid Id): IRequest<ProjectResponseDto>;
}
