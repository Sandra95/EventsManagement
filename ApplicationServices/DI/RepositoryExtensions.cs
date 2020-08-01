namespace ApplicationServices.DI
{
    using System.Diagnostics.CodeAnalysis;
    using DataRepository.Events;
    using DataRepository.EventsRegistrations;
    using DataRepository.Registrations;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public static class RepositoryExtensions
    {

        public static IServiceCollection InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEventsRepository, EventsRepository>();
            services.AddScoped<IRegistrationRepository, RegistrationRepository>();
            services.AddScoped<IEventsRegistrationsRepository, EventsRegistrationsRepository>();

            return services;
        }
    }
}
