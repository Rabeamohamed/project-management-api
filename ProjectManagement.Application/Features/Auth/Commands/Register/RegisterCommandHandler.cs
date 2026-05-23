using MediatR;
using ProjectManagement.Application.Common.Interfaces;
using ProjectManagement.Application.Features.Auth.DTOs;

namespace ProjectManagement.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler: IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request,CancellationToken cancellationToken)
    {
        var result =await _identityService.RegisterAsync(request.Email, request.Password);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors));
        }

        var token = await _identityService.LoginAsync(request.Email,request.Password);

        return new AuthResponseDto
        {
            Email = request.Email,
            Token = token!
        };
    }
}