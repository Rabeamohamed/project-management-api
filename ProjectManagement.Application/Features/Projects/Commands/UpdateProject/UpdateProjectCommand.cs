
using MediatR;
using ProjectManagement.Application.Features.Projects.DTOs;

namespace ProjectManagement.Application.Features.Projects.Commands.UpdateProject
{
    public record UpdateProjectCommand(Guid Id,string Name,string Description): IRequest<ProjectResponseDto>;
}
