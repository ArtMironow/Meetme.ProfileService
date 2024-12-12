using MapsterMapper;
using Meetme.ProfileService.BLL.Exceptions;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models.ProfileModels;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Meetme.ProfileService.BLL.Services;

public class ProfileService : IGenericService<ProfileModel, CreateProfileModel, UpdateProfileModel>
{
	private readonly IRepository<ProfileEntity> _repository;
	private readonly IMapper _mapper;
	private readonly ILogger<ProfileService> _logger;

	public ProfileService(IRepository<ProfileEntity> repository, IMapper mapper, ILogger<ProfileService> logger)
	{
		_repository = repository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task AddAsync(CreateProfileModel model, CancellationToken cancellationToken)
	{
		var profile = _mapper.Map<ProfileEntity>(model);

		var identityProfile = await _repository.GetFirstOrDefaultAsync(p => p.IdentityId == profile.IdentityId, cancellationToken);

		if (identityProfile != null)
		{
			_logger.LogWarning("Profile for identity with Id = {IdentityId} already exists", model.IdentityId);
			throw new BusinessLogicException("Profile for this identity already exists");
		}

		await _repository.AddAsync(profile, cancellationToken);
	}

	public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		var profile = await _repository.GetByIdAsync(id, cancellationToken);

		if (profile == null)
		{
			_logger.LogWarning("Profile with ID = {Id} was not found", id);
			throw new EntityNotFoundException("Profile with this id does not exist");
		}

		await _repository.RemoveAsync(profile, cancellationToken);
	}

	public async Task<IEnumerable<ProfileModel>> GetAllAsync(CancellationToken cancellationToken)
	{
		var profileEntities = await _repository.GetAllAsync(cancellationToken);

		var profiles = _mapper.Map<IEnumerable<ProfileModel>>(profileEntities);

		return profiles;
	}

	public async Task<ProfileModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var profile = await _repository.GetByIdAsync(id, cancellationToken);

		if (profile == null)
		{
			_logger.LogWarning("Profile with ID = {Id} was not found", id);
			throw new EntityNotFoundException("Profile with this id does not exist");
		}

		var profileModel = _mapper.Map<ProfileModel>(profile);

		return profileModel;
	}

	public async Task UpdateAsync(Guid id, UpdateProfileModel model, CancellationToken cancellationToken)
	{
		var profile = await _repository.GetByIdAsync(id, cancellationToken);

		if (profile == null)
		{
			_logger.LogWarning("Profile with ID = {Id} was not found", id);
			throw new EntityNotFoundException("Profile does not exist");
		}

		_mapper.Map(model, profile);

		await _repository.UpdateAsync(profile, cancellationToken);
	}
}
