using MapsterMapper;
using Meetme.ProfileService.BLL.Exceptions;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models.ProfileModels;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Entities.Enums;
using Meetme.ProfileService.DAL.Repositories.Interfaces;

namespace Meetme.ProfileService.BLL.Services;

public class ProfileService : IServiceOperations<ProfileModel, CreateProfileModel, UpdateProfileModel>
{
	private readonly IRepository<ProfileEntity> _repository;
	private readonly IMapper _mapper;

	public ProfileService(IRepository<ProfileEntity> repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task AddAsync(CreateProfileModel model, CancellationToken cancellationToken)
	{
		var profile = _mapper.Map<ProfileEntity>(model);

		await _repository.AddAsync(profile, cancellationToken);
	}

	public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		var profile = await _repository.GetByIdAsync(id, cancellationToken);

		if (profile == null)
		{
			throw new BusinessLogicException("Profile with this id does not exist");
		}

		await _repository.RemoveAsync(profile, cancellationToken);
	}

	public async Task<IEnumerable<ProfileModel>> GetAllAsync(CancellationToken cancellationToken)
	{
		var profiles = await _repository.GetAllAsync(cancellationToken);

		var listOfProfiles = _mapper.Map<IEnumerable<ProfileModel>>(profiles);

		return listOfProfiles;
	}

	public async Task<ProfileModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var profile = await _repository.GetByIdAsync(id, cancellationToken);

		if (profile == null)
		{
			throw new BusinessLogicException("Profile with this id does not exist");
		}

		var profileModel = _mapper.Map<ProfileModel>(profile);

		return profileModel;
	}

	public async Task UpdateAsync(Guid id, UpdateProfileModel model, CancellationToken cancellationToken)
	{
		var profile = await _repository.GetByIdAsync(id, cancellationToken);

		if (profile == null)
		{
			throw new BusinessLogicException("Profile does not exist");
		}

		profile.Name = model.Name;
		profile.Age = model.Age;
		profile.Bio = model.Bio;
		profile.Gender = Enum.Parse<Gender>(model.Gender!);
		profile.Location = model.Location;

		await _repository.UpdateAsync(profile, cancellationToken);
	}
}
