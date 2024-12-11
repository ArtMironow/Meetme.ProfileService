using Meetme.ProfileService.API.ViewModels.ProfileViewModels;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.IntegrationTests.Common.Attributes;
using Meetme.ProfileService.IntegrationTests.Common.Keys;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace Meetme.ProfileService.IntegrationTests;

public class ProfilesControllerTests : BaseIntegrationTest
{
	public ProfilesControllerTests(MeetmeProfileServiceWebApplicationFactory factory) : base(factory)
	{
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task CreateAsync_CreatesProfile(CreateProfileViewModel viewModel)
	{
		var response = await httpClient.PostAsJsonAsync(EndPointKeys.Profiles, viewModel, default);

		response.StatusCode.ShouldBe(HttpStatusCode.OK);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task CreateAsync_ReturnsBadRequest(CreateProfileViewModel invalidViewModel)
	{
		invalidViewModel.Name = string.Empty;

		var response = await httpClient.PostAsJsonAsync(EndPointKeys.Profiles, invalidViewModel, default);

		response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task GetAllAsync_ReturnsProfiles(IEnumerable<ProfileEntity> profileEntities)
	{
		var profileCount = profileEntities.Count();

		foreach (var profileEntity in profileEntities)
		{
			await dbContext.Set<ProfileEntity>().AddAsync(profileEntity, default);
		}

		await dbContext.SaveChangesAsync();

		var response = await httpClient.GetAsync(EndPointKeys.Profiles);

		response.StatusCode.ShouldBe(HttpStatusCode.OK);

		var responseContent = await response.Content.ReadFromJsonAsync<IEnumerable<ProfileViewModel>>();
		responseContent?.Count().ShouldBe(profileCount);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task GetByIdAsync_ReturnsProfile(ProfileEntity profileEntity)
	{
		await dbContext.Set<ProfileEntity>().AddAsync(profileEntity, default);
		await dbContext.SaveChangesAsync();

		var response = await httpClient.GetAsync(EndPointKeys.Profiles + $"/{profileEntity.Id}");

		response.StatusCode.ShouldBe(HttpStatusCode.OK);

		var responseContent = await response.Content.ReadFromJsonAsync<ProfileViewModel>();
		responseContent?.Name.ShouldBe(profileEntity.Name);
	}
	
	[Fact]
	public async Task GetByIdAsync_ReturnsNotFound()
	{
		var invalidId = Guid.NewGuid();

		var response = await httpClient.GetAsync(EndPointKeys.Profiles + $"/{invalidId}");

		response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task UpdateAsync_ReturnsNotFound(UpdateProfileViewModel updateProfileViewModel)
	{
		var invalidId = Guid.NewGuid();

		var response = await httpClient.PutAsJsonAsync(EndPointKeys.Profiles + $"/{invalidId}", updateProfileViewModel, default);

		response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task UpdateAsync_UpdatesProfile(UpdateProfileViewModel updateProfileViewModel, ProfileEntity profileEntity)
	{
		await dbContext.Set<ProfileEntity>().AddAsync(profileEntity, default);
		await dbContext.SaveChangesAsync();

		var response = await httpClient.PutAsJsonAsync(EndPointKeys.Profiles + $"/{profileEntity.Id}", updateProfileViewModel, default);

		response.StatusCode.ShouldBe(HttpStatusCode.OK);
	}

	[Fact]
	public async Task DeleteAsync_ReturnsNotFound()
	{
		var invalidId = Guid.NewGuid();

		var response = await httpClient.DeleteAsync(EndPointKeys.Profiles + $"/{invalidId}");

		response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task DeleteAsync_DeletesProfile(ProfileEntity profileEntity)
	{
		await dbContext.Set<ProfileEntity>().AddAsync(profileEntity, default);
		await dbContext.SaveChangesAsync();

		var response = await httpClient.DeleteAsync(EndPointKeys.Profiles + $"/{profileEntity.Id}");

		response.StatusCode.ShouldBe(HttpStatusCode.OK);
	}
}
