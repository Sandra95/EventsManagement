namespace EventsManagement.DI
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;


    [ExcludeFromCodeCoverage]
    internal static partial class ServicesExtensions
    {
        internal static IServiceCollection InitializeAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                //.SetupSettings(configuration)
                //.AddErrorHandling()
                //.SetupLogger()
                //.SetupSwagger()
                .SetupApplication()
                .SetupSql();
            //.SetupKafka(settings => configuration.Bind(KafkaSettingsConfig, settings))
            //.InitCrossCuttingAppServices();

            return services;
        }
    }
}
