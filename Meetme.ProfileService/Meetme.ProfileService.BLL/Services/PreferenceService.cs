using MapsterMapper;
using Meetme.ProfileService.BLL.Exceptions;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models.PreferenceModels;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Entities.Enums;
using Meetme.ProfileService.DAL.Repositories.Interfaces;

namespace Meetme.ProfileService.BLL.Services;

public class PreferenceService : IServiceOperations<PreferenceModel, CreatePreferenceModel, UpdatePreferenceModel>
{
	private readonly IRepository<PreferenceEntity> _repository;
	private readonly IMapper _mapper;

	public PreferenceService(IRepository<PreferenceEntity> repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task AddAsync(CreatePreferenceModel model, CancellationToken cancellationToken)
	{
		var preference = _mapper.Map<PreferenceEntity>(model);

		await _repository.AddAsync(preference, cancellationToken);
	}

	public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		var preference = await _repository.GetByIdAsync(id, cancellationToken);

		if (preference == null)
		{
			throw new BusinessLogicException("Preference with this id does not exist");
		}

		await _repository.RemoveAsync(preference, cancellationToken);
	}

	public async Task<IEnumerable<PreferenceModel>> GetAllAsync(CancellationToken cancellationToken)
	{
		var preferences = await _repository.GetAllAsync(cancellationToken);

		var listOfPreferences = _mapper.Map<IEnumerable<PreferenceModel>>(preferences);

		return listOfPreferences;
	}

	public async Task<PreferenceModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var preference = await _repository.GetByIdAsync(id, cancellationToken);

		if (preference == null)
		{
			throw new BusinessLogicException("Preference with this id does not exist");
		}

		var preferenceModel = _mapper.Map<PreferenceModel>(preference);

		return preferenceModel;
	}

	public async Task UpdateAsync(Guid id, UpdatePreferenceModel model, CancellationToken cancellationToken)
	{
		var preference = await _repository.GetByIdAsync(id, cancellationToken);

		if (preference == null)
		{
			throw new BusinessLogicException("Preference does not exist");
		}

		preference.MinAge = model.MinAge;
		preference.MaxAge = model.MaxAge;
		preference.GenderPreference = Enum.Parse<Gender>(model.GenderPreference!);
		preference.DistanceRadius = model.DistanceRadius;

		await _repository.UpdateAsync(preference, cancellationToken);
	}
}
