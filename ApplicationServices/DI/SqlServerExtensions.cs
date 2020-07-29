namespace ApplicationServices.DI
{
    using System.Diagnostics.CodeAnalysis;
    using DataRepository.DI;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public static class SqlServerExtensions
    {
        public static IServiceCollection InitializeSqlServerConfigurations(this IServiceCollection services)
        {
            return services.InitializeSqlServerContext();
        }
    }
}
