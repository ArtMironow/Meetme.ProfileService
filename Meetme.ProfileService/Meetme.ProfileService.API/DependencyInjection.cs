using FluentValidation;
using Mapster;
using MapsterMapper;
using Meetme.ProfileService.API.Middlewares;
using Meetme.ProfileService.API.ViewModels.ProfileViewModels;
using Meetme.ProfileService.BLL;
using System.Reflection;

namespace Meetme.ProfileService.API;

public static class DependencyInjection
{
	public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		services.AddMappings();

		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		services.AddBusinessLogicLayer(configuration);

		services.AddTransient<GlobalExceptionHandlingMiddleware>();

		return services;
	}

	private static IServiceCollection AddMappings(this IServiceCollection services)
	{
		var config = TypeAdapterConfig.GlobalSettings;
		config.Scan(Assembly.GetExecutingAssembly());

		services.AddSingleton(config);

		services.AddScoped<IMapper, ServiceMapper>();

		return services;
	}
}
