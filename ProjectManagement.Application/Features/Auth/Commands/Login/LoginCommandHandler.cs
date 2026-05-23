
using MediatR;
using ProjectManagement.Application.Common.Interfaces;
using ProjectManagement.Application.Features.Auth.DTOs;

namespace ProjectManagement.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly IIdentityService _identityService;
        public LoginCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<AuthResponseDto> Handle(LoginCommand request,CancellationToken cancellationToken)
        {
            var token = await _identityService.LoginAsync(request.Email,request.Password);

            if (token is null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            return new AuthResponseDto
            {
                Email = request.Email,
                Token = token
            };
        }
    }
}
