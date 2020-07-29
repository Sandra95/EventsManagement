namespace ApplicationServices.DI
{
    using System.Diagnostics.CodeAnalysis;
    using DataRepository.Events;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public static class RepositoryExtensions
    {

        public static IServiceCollection InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEventsRepository, EventsRepository>();

            return services;
        }
    }
}
