namespace ApplicationServices.DI
{
    using System.Diagnostics.CodeAnalysis;
    using ApplicationServices.Events;
    using ApplicationServices.Registrations;
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection InitializeApplicationsServices(this IServiceCollection services)
        {
            services.AddTransient<IEventsService, EventsService>();
            services.AddTransient<IRegistrationsService, RegistrationsService>();

            return services;
        }

        public static IServiceCollection InitializeAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(System.AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

    }
}
