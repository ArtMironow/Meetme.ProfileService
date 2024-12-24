using MapsterMapper;
using Meetme.ProfileService.BLL.Exceptions;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models.PreferenceModels;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Meetme.ProfileService.BLL.Services;

public class PreferenceService : IGenericService<PreferenceModel, CreatePreferenceModel, UpdatePreferenceModel>
{
	private readonly IRepository<PreferenceEntity> _repository;
	private readonly IMapper _mapper;
	private readonly ILogger<PreferenceService> _logger;

	public PreferenceService(IRepository<PreferenceEntity> repository, IMapper mapper, ILogger<PreferenceService> logger)
	{
		_repository = repository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task AddAsync(CreatePreferenceModel model, CancellationToken cancellationToken)
	{
		var profilePreference = await _repository.GetFirstOrDefaultAsync(p => p.ProfileId == model.ProfileId, cancellationToken);

		if (profilePreference != null)
		{
			_logger.LogWarning("Preference for profile with Id = {ProfileId} already exists", model.ProfileId);
			throw new BusinessLogicException("Preference for this profile already exists");
		}

		var preference = _mapper.Map<PreferenceEntity>(model);

		await _repository.AddAsync(preference, cancellationToken);
	}

	public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		var preference = await _repository.GetByIdAsync(id, cancellationToken);

		if (preference == null)
		{
			_logger.LogWarning("Preference with ID = {Id} was not found", id);
			throw new EntityNotFoundException("Preference with this id does not exist");
		}

		await _repository.RemoveAsync(preference, cancellationToken);
	}

	public async Task<IEnumerable<PreferenceModel>> GetAllAsync(CancellationToken cancellationToken)
	{
		var preferenceEntities = await _repository.GetAllAsync(cancellationToken);

		var preferences = _mapper.Map<IEnumerable<PreferenceModel>>(preferenceEntities);

		return preferences;
	}

	public async Task<PreferenceModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var preference = await _repository.GetByIdAsync(id, cancellationToken);

		if (preference == null)
		{
			_logger.LogWarning("Preference with ID = {Id} was not found", id);
			throw new EntityNotFoundException("Preference with this id does not exist");
		}

		var preferenceModel = _mapper.Map<PreferenceModel>(preference);

		return preferenceModel;
	}

	public async Task UpdateAsync(Guid id, UpdatePreferenceModel model, CancellationToken cancellationToken)
	{
		var preference = await _repository.GetByIdAsync(id, cancellationToken);

		if (preference == null)
		{
			_logger.LogWarning("Preference with ID = {Id} was not found", id);
			throw new EntityNotFoundException("Preference does not exist");
		}

		_mapper.Map(model, preference);

		await _repository.UpdateAsync(preference, cancellationToken);
	}
}
