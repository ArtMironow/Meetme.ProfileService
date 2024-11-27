using FluentValidation;
using Mapster;
using MapsterMapper;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models;
using Meetme.ProfileService.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Meetme.ProfileService.BLL;

public static class DependencyInjection
{
	public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
	{
		services.AddMappings();

		services.AddScoped<ICrud<ProfileModel>, Services.ProfileService>();
		services.AddScoped<ICrud<PreferenceModel>, PreferenceService>();
		services.AddScoped<ICrud<PhotoModel>, PhotoService>();

		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

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
