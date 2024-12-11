using Meetme.ProfileService.DAL.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Meetme.ProfileService.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<MeetmeProfileServiceWebApplicationFactory>
{
	private readonly IServiceScope _scope;
	protected readonly HttpClient httpClient;
	protected readonly ApplicationDbContext dbContext;
	protected BaseIntegrationTest(MeetmeProfileServiceWebApplicationFactory factory)
	{
		_scope = factory.Services.CreateScope();

		httpClient = factory.CreateClient();

		dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

		dbContext.Database.EnsureDeleted();
		dbContext.Database.EnsureCreated();
	}
}
