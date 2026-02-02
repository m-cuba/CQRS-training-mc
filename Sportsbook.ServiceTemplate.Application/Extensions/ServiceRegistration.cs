using Microsoft.Extensions.DependencyInjection;

namespace Sportsbook.ServiceTemplate.Application.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMapster();

        return services;
    }

    /// <summary>
    /// Insert Fluent Validation dependencies here...
    /// </summary>
    public static IServiceCollection AddValidationServices(this IServiceCollection services)
    {
        // e.g.: builder.Services.AddScoped<IValidator<Person>, PersonValidator>();

        return services;
    }


}