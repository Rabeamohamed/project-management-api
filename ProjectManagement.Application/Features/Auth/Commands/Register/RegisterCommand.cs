

using MediatR;
using ProjectManagement.Application.Features.Auth.DTOs;

namespace ProjectManagement.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(string Email,string Password): IRequest<AuthResponseDto>;
}
