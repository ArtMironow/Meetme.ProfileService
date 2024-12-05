using AutoFixture;
using MapsterMapper;
using Meetme.ProfileService.BLL.Exceptions;
using Meetme.ProfileService.BLL.Models.ProfileModels;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Repositories.Interfaces;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace Meetme.ProfileService.Tests.Services;

public class ProfileServiceTests
{
	private readonly BLL.Services.ProfileService _sut;
	private readonly IRepository<ProfileEntity> _profileRepositoryMock;
	private readonly IMapper _mapperMock;
	private readonly IFixture _fixture;

	public ProfileServiceTests()
	{
		_fixture = new Fixture();

		_fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
		_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

		_profileRepositoryMock = Substitute.For<IRepository<ProfileEntity>>();
		_mapperMock = Substitute.For<IMapper>();

		_sut = new BLL.Services.ProfileService(_profileRepositoryMock, _mapperMock);
	}

	[Fact]
	public async Task AddAsync_ShouldAddProfile_WhenIdentityIdIsUnique()
	{
		//Arrange
		var cancellationToken = _fixture.Create<CancellationToken>();

		var profileEntity = _fixture.Create<ProfileEntity>();
		var createProfileModel = _fixture.Create<CreateProfileModel>();

		_profileRepositoryMock.GetFirstOrDefaultAsync(Arg.Any<Expression<Func<ProfileEntity, bool>>>(), cancellationToken).ReturnsNull();

		_mapperMock.Map<ProfileEntity>(createProfileModel).Returns(profileEntity);

		//Act
		await _sut.AddAsync(createProfileModel, cancellationToken);

		//Assert
		await _profileRepositoryMock.Received(1).GetFirstOrDefaultAsync(Arg.Any<Expression<Func<ProfileEntity, bool>>>(), cancellationToken);
		await _profileRepositoryMock.Received(1).AddAsync(profileEntity, cancellationToken);
		
		_mapperMock.Received(1).Map<ProfileEntity>(createProfileModel);
	}

	[Fact]
	public async Task AddAsync_ShouldThrowBusinessLogicException_WhenIdentityIdIsNotUnique()
	{
		//Arrange
		var cancellationToken = _fixture.Create<CancellationToken>();

		var profileEntity = _fixture.Create<ProfileEntity>();
		var identityProfile = _fixture.Create<ProfileEntity?>();
		var createProfileModel = _fixture.Create<CreateProfileModel>();

		_profileRepositoryMock
			.GetFirstOrDefaultAsync(Arg.Any<Expression<Func<ProfileEntity, bool>>>(), cancellationToken)
			.Returns(identityProfile);

		_mapperMock.Map<ProfileEntity>(createProfileModel).Returns(profileEntity);

		//Act
		async Task action() => await _sut.AddAsync(createProfileModel, cancellationToken);

		//Assert
		var exception = await Assert.ThrowsAsync<BusinessLogicException>(action);
		Assert.Equal("Profile for this identity already exists", exception.Message);

		await _profileRepositoryMock.Received(1).GetFirstOrDefaultAsync(Arg.Any<Expression<Func<ProfileEntity, bool>>>(), cancellationToken);

		_mapperMock.Received(1).Map<ProfileEntity>(createProfileModel);
	}

	[Fact]
	public async Task DeleteAsync_ShouldDeleteProfile_WhenProfileExists()
	{
		//Arrange
		var cancellationToken = _fixture.Create<CancellationToken>();

		var id = _fixture.Create<Guid>();

		var profileEntity = _fixture.Create<ProfileEntity>();

		_profileRepositoryMock.GetByIdAsync(id, cancellationToken).Returns(profileEntity);

		//Act
		await _sut.DeleteAsync(id, cancellationToken);

		//Assert
		await _profileRepositoryMock.Received(1).RemoveAsync(profileEntity, cancellationToken);

		await _profileRepositoryMock.Received(1).GetByIdAsync(id, cancellationToken);
	}

	[Fact]
	public async Task DeleteAsync_ShouldThrowBusinessLogicException_WhenProfileDoesNotExist()
	{
		//Arrange
		var cancellationToken = _fixture.Create<CancellationToken>();

		var id = _fixture.Create<Guid>();

		_profileRepositoryMock.GetByIdAsync(id, cancellationToken).ReturnsNull();

		//Act
		async Task action() => await _sut.DeleteAsync(id, cancellationToken);

		//Assert
		var exception = await Assert.ThrowsAsync<BusinessLogicException>(action);
		Assert.Equal("Profile with this id does not exist", exception.Message);

		await _profileRepositoryMock.Received(1).GetByIdAsync(id, cancellationToken);
	}

	[Fact]
	public async Task UpdateAsync_ShouldDeleteProfile_WhenProfileExists()
	{
		//Arrange
		var cancellationToken = _fixture.Create<CancellationToken>();

		var id = _fixture.Create<Guid>();

		var profileEntity = _fixture.Create<ProfileEntity>();
		var updateProfileModel = _fixture.Create<UpdateProfileModel>();

		_profileRepositoryMock.GetByIdAsync(id, cancellationToken).Returns(profileEntity);

		//Act
		await _sut.UpdateAsync(id, updateProfileModel, cancellationToken);

		//Assert
		await _profileRepositoryMock.Received(1).GetByIdAsync(id, cancellationToken);
		await _profileRepositoryMock.Received(1).UpdateAsync(profileEntity, cancellationToken);

		_mapperMock.Received(1).Map(updateProfileModel, profileEntity);
	}

	[Fact]
	public async Task UpdateAsync_ShouldThrowBusinessLogicException_WhenProfileDoesNotExist()
	{
		//Arrange
		var cancellationToken = _fixture.Create<CancellationToken>();

		var id = _fixture.Create<Guid>();

		var updateProfileModel = _fixture.Create<UpdateProfileModel>();

		_profileRepositoryMock.GetByIdAsync(id, cancellationToken).ReturnsNull();

		//Act
		async Task action() => await _sut.UpdateAsync(id, updateProfileModel, cancellationToken);

		//Assert
		var exception = await Assert.ThrowsAsync<BusinessLogicException>(action);
		Assert.Equal("Profile does not exist", exception.Message);

		await _profileRepositoryMock.Received(1).GetByIdAsync(id, cancellationToken);
	}

	[Fact]
	public async Task GetAllAsync_ShouldReturnProfileModels_WhenProfilesExist()
	{
		//Arrange
		var cancellationToken = _fixture.Create<CancellationToken>();

		var profileEntities = _fixture.CreateMany<ProfileEntity>(10);
		var expectedProfiles = _fixture.CreateMany<ProfileModel>(10);

		_profileRepositoryMock.GetAllAsync(cancellationToken).Returns(profileEntities);

		_mapperMock.Map<IEnumerable<ProfileModel>>(profileEntities).Returns(expectedProfiles);

		//Act
		var resultingProfiles = await _sut.GetAllAsync(cancellationToken);

		//Assert
		Assert.Equal(expectedProfiles, resultingProfiles);

		await _profileRepositoryMock.Received(1).GetAllAsync(cancellationToken);

		_mapperMock.Received(1).Map<IEnumerable<ProfileModel>>(profileEntities);
	}

	[Fact]
	public async Task GetAllAsync_ShouldReturnEmptyList_WhenProfilesDoNotExist()
	{
		//Arrange
		var cancellationToken = _fixture.Create<CancellationToken>();

		var profileEntities = Enumerable.Empty<ProfileEntity>();
		var expectedProfiles = Enumerable.Empty<ProfileModel>();

		_profileRepositoryMock.GetAllAsync(cancellationToken).Returns(profileEntities);

		_mapperMock.Map<IEnumerable<ProfileModel>>(profileEntities).Returns(expectedProfiles);

		//Act
		var resultingProfiles = await _sut.GetAllAsync(cancellationToken);

		//Assert
		Assert.Empty(resultingProfiles);

		await _profileRepositoryMock.Received(1).GetAllAsync(cancellationToken);

		_mapperMock.Received(1).Map<IEnumerable<ProfileModel>>(profileEntities);
	}

	[Fact]
	public async Task GetByIdAsync_ShouldReturnProfileModel_WhenProfileExists()
	{
		//Arrange
		var id = _fixture.Create<Guid>();
		var cancellationToken = _fixture.Create<CancellationToken>();
		var profileEntity = _fixture.Create<ProfileEntity>();
		var expectedProfileModel = _fixture.Create<ProfileModel>();

		_profileRepositoryMock.GetByIdAsync(id, cancellationToken).Returns(profileEntity);

		_mapperMock.Map<ProfileModel>(profileEntity).Returns(expectedProfileModel);

		//Act
		var resultingProfile = await _sut.GetByIdAsync(id, cancellationToken);

		//Assert
		Assert.Equal(expectedProfileModel, resultingProfile);

		await _profileRepositoryMock.Received(1).GetByIdAsync(id, cancellationToken);

		_mapperMock.Received(1).Map<ProfileModel>(profileEntity);
	}

	[Fact]
	public async Task GetByIdAsync_ShouldThrowBusinessLogicException_WhenProfileDoesNotExist()
	{
		//Arrange
		var id = _fixture.Create<Guid>();
		var cancellationToken = _fixture.Create<CancellationToken>();

		_profileRepositoryMock.GetByIdAsync(id, cancellationToken).ReturnsNull();

		//Act
		async Task<ProfileModel> action() => await _sut.GetByIdAsync(id, cancellationToken);

		//Assert
		var exception = await Assert.ThrowsAsync<BusinessLogicException>((Func<Task<ProfileModel>>)action);
		Assert.Equal("Profile with this id does not exist", exception.Message);

		await _profileRepositoryMock.Received(1).GetByIdAsync(id, cancellationToken);
	}
}
