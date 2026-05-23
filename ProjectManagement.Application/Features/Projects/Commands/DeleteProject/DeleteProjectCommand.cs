using MediatR;

namespace ProjectManagement.Application.Features.Projects.Commands.DeleteProject
{
    public record DeleteProjectCommand(Guid Id): IRequest;
}
