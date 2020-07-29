namespace ApplicationServices.DI
{
	using System;
	using System.Diagnostics.CodeAnalysis;

	[ExcludeFromCodeCoverage]
	public static class ApplicationServicesExtensions
	{
		public static IServiceCollection InitializeApplicationsServices(this IServiceCollection services)
		{
			services.AddTransient<IEventsService, EventsService>();

			return service;
		}


	}
}
