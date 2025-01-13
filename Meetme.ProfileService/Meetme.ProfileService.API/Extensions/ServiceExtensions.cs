using Meetme.ProfileService.API.Common.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Meetme.ProfileService.API.Extensions;

public static class ServiceExtensions
{
	public static void ConfigureAuth(this IServiceCollection services)
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
	}
}

