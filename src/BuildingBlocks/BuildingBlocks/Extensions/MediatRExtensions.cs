using System.Reflection;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddBuildingBlocks(this IServiceCollection services, Assembly assembly)
    {
        
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }
}