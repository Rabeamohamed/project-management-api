
using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Application.Common.Interfaces;
using ProjectManagement.Application.Common.Models;
using ProjectManagement.Infrastructure.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;

namespace ProjectManagement.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;
        public TokenService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(AuthUser user)
        {
           var claims = new List<Claim>
           {
               new(ClaimTypes.NameIdentifier,user.Id),
               new( ClaimTypes.Email,user.Email)
           };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: _jwtOptions.Issuer,audience: _jwtOptions.Audience, claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes), signingCredentials: creds);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
