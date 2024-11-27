using MapsterMapper;
using Meetme.ProfileService.BLL.Exceptions;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Repositories.Interfaces;

namespace Meetme.ProfileService.BLL.Services;

public class PreferenceService : ICrud<PreferenceModel>
{
	private readonly IRepository<PreferenceEntity> _repository;
	private readonly IMapper _mapper;

	public PreferenceService(IRepository<PreferenceEntity> repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task AddAsync(PreferenceModel model, CancellationToken cancellationToken)
	{
		var preference = _mapper.Map<PreferenceEntity>(model);

		await _repository.AddAsync(preference, cancellationToken);
	}

	public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		var preference = await _repository.GetByIdAsync(id, cancellationToken);

		if (preference == null)
		{
			throw new ProfileServiceException("Preference with this id does not exist");
		}

		await _repository.RemoveAsync(preference, cancellationToken);
	}

	public async Task<IEnumerable<PreferenceModel>> GetAllAsync(CancellationToken cancellationToken)
	{
		var preferences = await _repository.GetAllAsync(cancellationToken);

		var listOfPreferences = new List<PreferenceModel>();

		foreach (var preference in preferences)
		{
			var preferenceModel = _mapper.Map<PreferenceModel>(preference);
			listOfPreferences.Add(preferenceModel);
		}

		return listOfPreferences;
	}

	public async Task<PreferenceModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var preference = await _repository.GetByIdAsync(id, cancellationToken);

		if (preference == null)
		{
			throw new ProfileServiceException("Preference with this id does not exist");
		}

		var preferenceModel = _mapper.Map<PreferenceModel>(preference);

		return preferenceModel;
	}

	public async Task UpdateAsync(PreferenceModel model, CancellationToken cancellationToken)
	{
		var preference = _mapper.Map<PreferenceEntity>(model);

		await _repository.UpdateAsync(preference, cancellationToken);
	}
}
