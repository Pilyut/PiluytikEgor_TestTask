using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using Application.Services.Interfaces;
using Infrastructure.Repositories.Interfaces;

namespace Application
{
    public static class ApplicationConfigExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .AddAutoMapper()
                .AddServices();
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var types = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(x => x.FullName.StartsWith("Application"))
                .SelectMany(x => x.GetTypes()
                    .Where(type => type.IsClass
                                   && !type.IsAbstract
                                   && type.IsSubclassOf(typeof(Profile))))
                .ToArray();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(types);
                cfg.DisableConstructorMapping();
            });

            services.AddSingleton(mapperConfig.CreateMapper());

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            var types = Assembly

                .GetExecutingAssembly()
                .GetTypes();

            var interfaceTypes = types
                .Where(type => type.IsInterface
                            && (type.Namespace == typeof(IPersonService).Namespace))
            .ToArray();

            foreach (var interfaceType in interfaceTypes)
            {
                var implementation = types
                    .FirstOrDefault(type => type.GetInterface(interfaceType.Name) == interfaceType);

                services
                    .AddScoped(interfaceType, implementation);
            }

            return services;
        }
    }
}
