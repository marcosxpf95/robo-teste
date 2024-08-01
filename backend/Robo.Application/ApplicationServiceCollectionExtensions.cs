using Microsoft.Extensions.DependencyInjection;
using Robo.Application.Repositories;
using Robo.Application.Services;

namespace Movies.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IRoboRepository, RoboRepository>();
        services.AddSingleton<IRoboServices, RoboServices>();

        return services;
    }
}
