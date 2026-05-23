

namespace ProjectManagement.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<(bool Succeeded, IEnumerable<string> Errors)>
        RegisterAsync(string email,string password);
        Task<string?> LoginAsync(string email,string password);
    }
}
