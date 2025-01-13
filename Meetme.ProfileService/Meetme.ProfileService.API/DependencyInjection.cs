using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Meetme.ProfileService.API.Middlewares;
using Meetme.ProfileService.BLL;
using System.Reflection;
using System.Text.Json.Serialization;
using Meetme.ProfileService.API.Extensions;

namespace Meetme.ProfileService.API;

public static class DependencyInjection
{
	public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddControllers().AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
		});
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		services.AddMappings();

		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		services.ConfigureAuth();
		services.AddAuthorization();

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
