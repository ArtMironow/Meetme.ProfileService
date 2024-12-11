using Meetme.ProfileService.DAL.Data;
using Meetme.ProfileService.IntegrationTests.Common.Keys;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Meetme.ProfileService.IntegrationTests;

public class MeetmeProfileServiceWebApplicationFactory : WebApplicationFactory<Program>
{
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureAppConfiguration((context, config) =>
		{
			config.AddJsonFile(ConfigurationKeys.AppSettings).AddEnvironmentVariables();
		});

		builder.ConfigureTestServices(services =>
		{
			services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

			string? connectionString = GetConnectionString(services);

			services.AddDbContext<ApplicationDbContext>(
				options => options
				.UseNpgsql(connectionString)
				.AddInterceptors(new TimestampInterceptor()));
		});
	}

	private static string? GetConnectionString(IServiceCollection services)
	{
		var serviceProvider = services.BuildServiceProvider();
		var configuration = serviceProvider.GetRequiredService<IConfiguration>();
		var connectionString = configuration.GetConnectionString(ConfigurationKeys.ConnectionString);

		return connectionString;
	}
}
