using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Application.Common.Interfaces;
using ProjectManagement.Infrastructure.Data;
using ProjectManagement.Infrastructure.Identity;
using ProjectManagement.Infrastructure.Options;
using ProjectManagement.Infrastructure.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;


namespace ProjectManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var jwtOptions = configuration
                .GetSection(JwtOptions.SectionName)
                .Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions!.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(jwtOptions.Key))
                    };
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            //Regiser Redis Cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration =configuration.GetConnectionString("Redis");
            });

            services.AddScoped<ICacheService, CacheService>();

            return services;
        }
    }
}
