using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Common.Behaviors;
using System.Reflection;

namespace ProjectManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));

        return services;
    }
}