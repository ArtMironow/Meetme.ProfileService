using MapsterMapper;
using Meetme.ProfileService.BLL.Exceptions;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Repositories;
using Meetme.ProfileService.DAL.Repositories.Interfaces;

namespace Meetme.ProfileService.BLL.Services;

public class ProfileService : ICrud<ProfileModel>
{
	private readonly IRepository<ProfileEntity> _repository;
	private readonly IMapper _mapper;

	public ProfileService(IRepository<ProfileEntity> repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task AddAsync(ProfileModel model, CancellationToken cancellationToken)
	{
		var profile = _mapper.Map<ProfileEntity>(model);

		await _repository.AddAsync(profile, cancellationToken);
	}

	public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		var profile = await _repository.GetByIdAsync(id, cancellationToken);

		if (profile == null)
		{
			throw new ProfileServiceException("Profile with this id does not exist");
		}

		await _repository.RemoveAsync(profile, cancellationToken);
	}

	public async Task<IEnumerable<ProfileModel>> GetAllAsync(CancellationToken cancellationToken)
	{
		var profiles = await _repository.GetAllAsync(cancellationToken);

		var listOfProfiles = new List<ProfileModel>();

		foreach (var profile in profiles)
		{
			var profileModel = _mapper.Map<ProfileModel>(profile);
			listOfProfiles.Add(profileModel);
		}

		return listOfProfiles;
	}

	public async Task<ProfileModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var profile = await _repository.GetByIdAsync(id, cancellationToken);

		if (profile == null)
		{
			throw new ProfileServiceException("Profile with this id does not exist");
		}

		var profileModel = _mapper.Map<ProfileModel>(profile);

		return profileModel;
	}

	public async Task UpdateAsync(ProfileModel model, CancellationToken cancellationToken)
	{
		var profile = _mapper.Map<ProfileEntity>(model);

		await _repository.UpdateAsync(profile, cancellationToken);
	}
}
