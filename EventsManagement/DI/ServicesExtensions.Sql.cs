namespace EventsManagement.DI
{
    using ApplicationServices.DI;
    using Microsoft.Extensions.DependencyInjection;

    internal static partial class ServicesExtensions
    {
        private static IServiceCollection SetupSql(this IServiceCollection services)
        {
            return services.InitializeSqlServerConfigurations();
        }
    }
}
