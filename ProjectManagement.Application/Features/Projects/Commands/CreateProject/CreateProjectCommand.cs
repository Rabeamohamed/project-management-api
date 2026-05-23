
using MediatR;
using ProjectManagement.Application.Features.Projects.DTOs;

namespace ProjectManagement.Application.Features.Projects.Commands.CreateProject
{

    public record CreateProjectCommand(string Name,string Description): IRequest<ProjectResponseDto>;
}
