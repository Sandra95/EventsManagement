namespace DataRepository.DI
{
    using System.Diagnostics.CodeAnalysis;
    using DataRepository.EFContext;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public static class SqlServerContextExtensions
    {
        public static IServiceCollection InitializeSqlServerContext(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            //TODO: var sqlServerSettings = serviceProvider.GetService<ISqlServerSettings>();
            var connectionString = @"Data Source=localhost;Initial Catalog=SVC_EVENTS_MANAGER;Integrated Security=True;Application Name=SVC_EVENTS_MANAGER;";
            services.AddDbContext<EventsContext>(options => options
                                                            .UseLazyLoadingProxies(false)
                                                            .UseSqlServer(connectionString));

            return services;
        }
    }
}
