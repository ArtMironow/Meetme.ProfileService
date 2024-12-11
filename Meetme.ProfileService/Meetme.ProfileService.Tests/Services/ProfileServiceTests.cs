using Mapster;
using MapsterMapper;
using Meetme.ProfileService.API.Mapping;
using Meetme.ProfileService.BLL.Exceptions;
using Meetme.ProfileService.BLL.Models.ProfileModels;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Repositories.Interfaces;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using System.Linq.Expressions;

namespace Meetme.ProfileService.Tests.Services;

public class ProfileServiceTests
{
	private readonly BLL.Services.ProfileService _profileService;
	private readonly IRepository<ProfileEntity> _profileRepositoryMock;
	private readonly IMapper _mapper;

	public ProfileServiceTests()
	{
		_profileRepositoryMock = Substitute.For<IRepository<ProfileEntity>>();

		var config = new TypeAdapterConfig();
		new MappingConfig().Register(config);

		_mapper = new Mapper(config);

		_profileService = new BLL.Services.ProfileService(_profileRepositoryMock, _mapper);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task AddAsync_ShouldAddProfile_WhenIdentityIdIsUnique(CreateProfileModel createProfileModel)
	{
		_profileRepositoryMock.GetFirstOrDefaultAsync(Arg.Any<Expression<Func<ProfileEntity, bool>>>(), default).ReturnsNull();

		await _profileService.AddAsync(createProfileModel, default);

		await _profileRepositoryMock.Received(1).GetFirstOrDefaultAsync(Arg.Any<Expression<Func<ProfileEntity, bool>>>(), default);
		await _profileRepositoryMock.Received(1).AddAsync(Arg.Any<ProfileEntity>(), default);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task AddAsync_ShouldThrowBusinessLogicException_WhenIdentityIdIsNotUnique(
		ProfileEntity identityProfile,
		CreateProfileModel createProfileModel)
	{
		_profileRepositoryMock
			.GetFirstOrDefaultAsync(Arg.Any<Expression<Func<ProfileEntity, bool>>>(), default)
			.Returns(identityProfile);

		await Should.ThrowAsync<BusinessLogicException>(
			async () => await _profileService.AddAsync(createProfileModel, default));

		await _profileRepositoryMock.Received(1).GetFirstOrDefaultAsync(Arg.Any<Expression<Func<ProfileEntity, bool>>>(), default);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task DeleteAsync_ShouldDeleteProfile_WhenProfileExists(ProfileEntity profileEntity)
	{
		var id = Guid.NewGuid();

		_profileRepositoryMock.GetByIdAsync(id, default).Returns(profileEntity);

		await _profileService.DeleteAsync(id, default);

		await _profileRepositoryMock.Received(1).RemoveAsync(profileEntity, default);
		await _profileRepositoryMock.Received(1).GetByIdAsync(id, default);
	}

	[Fact]
	public async Task DeleteAsync_ShouldThrowKeyNotFoundException_WhenProfileDoesNotExist()
	{
		var id = Guid.NewGuid();

		_profileRepositoryMock.GetByIdAsync(id, default).ReturnsNull();

		await Should.ThrowAsync<EntityNotFoundException>(
			async () => await _profileService.DeleteAsync(id, default));

		await _profileRepositoryMock.Received(1).GetByIdAsync(id, default);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task UpdateAsync_ShouldUpdateProfile_WhenProfileExists(
		ProfileEntity profileEntity,
		UpdateProfileModel updateProfileModel)
	{
		var id = Guid.NewGuid();

		_profileRepositoryMock.GetByIdAsync(id, default).Returns(profileEntity);

		await _profileService.UpdateAsync(id, updateProfileModel, default);

		await _profileRepositoryMock.Received(1).GetByIdAsync(id, default);
		await _profileRepositoryMock.Received(1).UpdateAsync(profileEntity, default);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task UpdateAsync_ShouldThrowBusinessLogicException_WhenProfileDoesNotExist(UpdateProfileModel updateProfileModel)
	{
		var id = Guid.NewGuid();

		_profileRepositoryMock.GetByIdAsync(id, default).ReturnsNull();

		await Should.ThrowAsync<EntityNotFoundException>(
			async () => await _profileService.UpdateAsync(id, updateProfileModel, default));

		await _profileRepositoryMock.Received(1).GetByIdAsync(id, default);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task GetAllAsync_ShouldReturnProfileModels_WhenProfilesExist(IEnumerable<ProfileEntity> profileEntities)
	{
		_profileRepositoryMock.GetAllAsync(default).Returns(profileEntities);
		var expectedProfiles = _mapper.Map<IEnumerable<ProfileModel>>(profileEntities);

		var resultingProfiles = await _profileService.GetAllAsync(default);

		resultingProfiles.ShouldBeEquivalentTo(expectedProfiles);

		await _profileRepositoryMock.Received(1).GetAllAsync(default);
	}

	[Fact]
	public async Task GetAllAsync_ShouldReturnEmptyList_WhenProfilesDoNotExist()
	{
		var profileEntities = Enumerable.Empty<ProfileEntity>();

		_profileRepositoryMock.GetAllAsync(default).Returns(profileEntities);
		var expectedProfiles = _mapper.Map<IEnumerable<ProfileModel>>(profileEntities);

		var resultingProfiles = await _profileService.GetAllAsync(default);

		resultingProfiles.ShouldBeEquivalentTo(expectedProfiles);

		await _profileRepositoryMock.Received(1).GetAllAsync(default);
	}

	[Theory, OmitOnRecursionAutoData]
	public async Task GetByIdAsync_ShouldReturnProfileModel_WhenProfileExists(ProfileEntity profileEntity)
	{
		var id = Guid.NewGuid();

		_profileRepositoryMock.GetByIdAsync(id, default).Returns(profileEntity);
		var expectedProfileModel = _mapper.Map<ProfileModel>(profileEntity);

		var resultingProfile = await _profileService.GetByIdAsync(id, default);

		resultingProfile.ShouldBeEquivalentTo(expectedProfileModel);

		await _profileRepositoryMock.Received(1).GetByIdAsync(id, default);
	}

	[Fact]
	public async Task GetByIdAsync_ShouldThrowBusinessLogicException_WhenProfileDoesNotExist()
	{
		var id = Guid.NewGuid();

		_profileRepositoryMock.GetByIdAsync(id, default).ReturnsNull();

		await Should.ThrowAsync<EntityNotFoundException>(
			async () => await _profileService.GetByIdAsync(id, default));

		await _profileRepositoryMock.Received(1).GetByIdAsync(id, default);
	}
}
