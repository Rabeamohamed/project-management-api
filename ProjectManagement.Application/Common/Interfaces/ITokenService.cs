using ProjectManagement.Application.Common.Models;

namespace ProjectManagement.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AuthUser user);

    }
}
