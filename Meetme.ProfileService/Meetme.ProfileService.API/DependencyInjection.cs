using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Meetme.ProfileService.API.Middlewares;
using Meetme.ProfileService.BLL;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Meetme.ProfileService.API.Common.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

	private static IServiceCollection ConfigureAuth(this IServiceCollection services)
	{
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.Authority = AuthKeys.Authority;
				options.Audience = AuthKeys.Audience;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					NameClaimType = ClaimTypes.NameIdentifier
				};
			});

		return services;
	}
}
