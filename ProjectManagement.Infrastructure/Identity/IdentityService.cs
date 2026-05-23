using Microsoft.AspNetCore.Identity;
using ProjectManagement.Application.Common.Interfaces;
using ProjectManagement.Application.Common.Models;

namespace ProjectManagement.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly ITokenService _tokenService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<(bool Succeeded,
        IEnumerable<string> Errors)>
        RegisterAsync(
            string email,
            string password)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(
            user,
            password);

        if (!result.Succeeded)
        {
            return (
                false,
                result.Errors.Select(x => x.Description));
        }

        return (true, Enumerable.Empty<string>());
    }

    public async Task<string?> LoginAsync(
        string email,
        string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return null;

        var result =
            await _signInManager.CheckPasswordSignInAsync(
                user,
                password,
                false);

        if (!result.Succeeded)
            return null;

        return _tokenService.GenerateToken(
            new AuthUser
            {
                Id = user.Id,
                Email = user.Email!
            });
    }
}