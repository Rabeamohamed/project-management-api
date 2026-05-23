
using MediatR;
using ProjectManagement.Application.Features.Auth.DTOs;

namespace ProjectManagement.Application.Features.Auth.Commands.Login
{
    public record LoginCommand(string Email, string Password): IRequest<AuthResponseDto>;
}
