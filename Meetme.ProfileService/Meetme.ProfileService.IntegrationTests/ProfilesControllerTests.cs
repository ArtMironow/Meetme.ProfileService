using Meetme.ProfileService.API.ViewModels.ProfileViewModels;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.IntegrationTests.Common.Attributes;
using Meetme.ProfileService.IntegrationTests.Common.Keys;
using Microsoft.EntityFrameworkCore;
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
	public async Task CreateAsync_CreatesProfile_WhenProfileIsValid(CreateProfileViewModel viewModel)
	{
		var response = await httpClient.PostAsJsonAsync(EndPointKeys.Profiles, viewModel, default);

		response.StatusCode.ShouldBe(HttpStatusCode.OK);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task CreateAsync_ReturnsBadRequest_WhenProfileIsNotValid(CreateProfileViewModel invalidViewModel)
	{
		invalidViewModel.Name = string.Empty;

		var response = await httpClient.PostAsJsonAsync(EndPointKeys.Profiles, invalidViewModel, default);

		response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task GetAllAsync_ReturnsProfiles_WhenProfilesExist(IEnumerable<ProfileEntity> profileEntities)
	{
		var profileCount = profileEntities.Count();

		await InitializeProfileData(profileEntities);

		var response = await httpClient.GetAsync(EndPointKeys.Profiles);

		response.StatusCode.ShouldBe(HttpStatusCode.OK);

		var responseContent = await response.Content.ReadFromJsonAsync<IEnumerable<ProfileViewModel>>();
		responseContent?.Count().ShouldBe(profileCount);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task GetByIdAsync_ReturnsProfile_WhenProfileExists(ProfileEntity profileEntity)
	{
		await InitializeProfileData(profileEntity);

		var response = await httpClient.GetAsync(EndPointKeys.Profiles + $"/{profileEntity.Id}");

		response.StatusCode.ShouldBe(HttpStatusCode.OK);

		var responseContent = await response.Content.ReadFromJsonAsync<ProfileViewModel>();
		responseContent?.Name.ShouldBe(profileEntity.Name);
	}

	[Fact]
	public async Task GetByIdAsync_ReturnsNotFound_WhenProfileDoesNotExist()
	{
		var invalidId = Guid.NewGuid();

		var response = await httpClient.GetAsync(EndPointKeys.Profiles + $"/{invalidId}");

		response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task UpdateAsync_ReturnsNotFound_WhenProfileDoesNotExist(UpdateProfileViewModel updateProfileViewModel)
	{
		var invalidId = Guid.NewGuid();

		var response = await httpClient.PutAsJsonAsync(EndPointKeys.Profiles + $"/{invalidId}", updateProfileViewModel, default);

		response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task UpdateAsync_UpdatesProfile_WhenProfileExists(UpdateProfileViewModel updateProfileViewModel, ProfileEntity profileEntity)
	{
		await InitializeProfileData(profileEntity);

		var response = await httpClient.PutAsJsonAsync(EndPointKeys.Profiles + $"/{profileEntity.Id}", updateProfileViewModel, default);

		response.StatusCode.ShouldBe(HttpStatusCode.OK);

		var updatedProfile = await GetProfileById(profileEntity.Id);

		updatedProfile?.Name.ShouldBe(updateProfileViewModel.Name);
		updatedProfile?.Age.ShouldBe(updateProfileViewModel.Age);
		updatedProfile?.Gender.ShouldBe(updateProfileViewModel.Gender);
		updatedProfile?.Bio.ShouldBe(updateProfileViewModel.Bio);
		updatedProfile?.Location.ShouldBe(updateProfileViewModel.Location);
	}

	[Fact]
	public async Task DeleteAsync_ReturnsNotFound_WhenProfileDoesNotExist()
	{
		var invalidId = Guid.NewGuid();

		var response = await httpClient.DeleteAsync(EndPointKeys.Profiles + $"/{invalidId}");

		response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task DeleteAsync_DeletesProfile_WhenProfileExists(ProfileEntity profileEntity)
	{
		await InitializeProfileData(profileEntity);

		var response = await httpClient.DeleteAsync(EndPointKeys.Profiles + $"/{profileEntity.Id}");

		response.StatusCode.ShouldBe(HttpStatusCode.OK);
	}

	private async Task InitializeProfileData(object profiles)
	{
		if (profiles is ProfileEntity profileEntity)
		{
			await dbContext.Set<ProfileEntity>().AddAsync(profileEntity, default);
		}
		else if (profiles is IEnumerable<ProfileEntity> profileEntities)
		{
			await dbContext.Set<ProfileEntity>().AddRangeAsync(profileEntities, default);
		}
		
		await dbContext.SaveChangesAsync();
	}

	private Task<ProfileEntity?> GetProfileById(Guid id)
	{
		return dbContext.Set<ProfileEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
	}
}
