using Meetme.ProfileService.API.Middlewares;
using Meetme.ProfileService.BLL;

namespace Meetme.ProfileService.API;

public static class DependencyInjection
{
	public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		services.AddBusinessLogicLayer(configuration);

		services.AddTransient<GlobalExceptionHandlingMiddleware>();

		return services;
	}
}
