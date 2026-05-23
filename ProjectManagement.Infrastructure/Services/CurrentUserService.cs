
using Microsoft.AspNetCore.Http;
using ProjectManagement.Application.Common.Interfaces;
using System.Security.Claims;

namespace ProjectManagement.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor =httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?
                .User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}