using FluentValidation;
using Mapster;
using MapsterMapper;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models.PhotoModels;
using Meetme.ProfileService.BLL.Models.PreferenceModels;
using Meetme.ProfileService.BLL.Models.ProfileModels;
using Meetme.ProfileService.BLL.Services;
using Meetme.ProfileService.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Meetme.ProfileService.BLL;

public static class DependencyInjection
{
	public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDataAccessLayer(configuration);

		services.AddMappings();

		services.AddScoped<IGenericService<ProfileModel, CreateProfileModel, UpdateProfileModel>, Services.ProfileService>();
		services.AddScoped<IGenericService<PreferenceModel, CreatePreferenceModel, UpdatePreferenceModel>, PreferenceService>();
		services.AddScoped<IGenericService<PhotoModel, CreatePhotoModel, UpdatePhotoModel>, PhotoService>();

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
