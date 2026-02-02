using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Sportsbook.ServiceTemplate.Application.DTOs;
using System.Reflection;

namespace Sportsbook.ServiceTemplate.Application.Extensions
{
    public static class MapsterConfiguration
    {
        public static void AddMapster(this IServiceCollection services)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            Assembly applicationAssembly = typeof(BaseDto<,>).Assembly;
            typeAdapterConfig.Scan(applicationAssembly);
        }
    }
}
