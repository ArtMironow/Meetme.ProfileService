using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Meetme.ProfileService.DAL.Repositories.Interfaces;
using Meetme.ProfileService.DAL.Repositories;
using Meetme.ProfileService.DAL.Data;

namespace Meetme.ProfileService.DAL;

public static class DependencyInjection
{
	public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<TimestampInterceptor>();

		services.AddDbContext<ApplicationDbContext>(
			(sp, options) => options
				.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
				.AddInterceptors(sp.GetRequiredService<TimestampInterceptor>()));

		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

		services.AddScoped<IProfileRepository, ProfileRepository>();

		return services;
	}
}
