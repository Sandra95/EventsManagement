namespace EventsManagement.DI
{
    using ApplicationServices.DI;
    using Microsoft.Extensions.DependencyInjection;

    internal static partial class ServicesExtensions
    {
        private static IServiceCollection SetupApplication(this IServiceCollection services)
        {
            services.InitializeRepositories();
            services.InitializeApplicationsServices();
            services.InitializeAutoMapperProfiles();

            return services;
        }
    }
}
