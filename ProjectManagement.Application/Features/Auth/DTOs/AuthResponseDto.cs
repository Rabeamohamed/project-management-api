

namespace ProjectManagement.Application.Features.Auth.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
