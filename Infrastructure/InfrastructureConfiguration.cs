using Infrastructure.DataBase;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            string connectionString)
        {
            services
               .AddDbContext<PersonsDbContext>(options => options.UseSqlite(connectionString));

            services
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes();

            var interfaceTypes = types
                .Where(type => type.IsInterface
                            && (type.Namespace == typeof(IPersonRepository).Namespace))
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
