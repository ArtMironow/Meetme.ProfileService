using MapsterMapper;
using Meetme.ProfileService.BLL.Exceptions;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Repositories.Interfaces;

namespace Meetme.ProfileService.BLL.Services;

public class PhotoService : ICrud<PhotoModel>
{
	private readonly IRepository<PhotoEntity> _repository;
	private readonly IMapper _mapper;

	public PhotoService(IRepository<PhotoEntity> repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task AddAsync(PhotoModel model, CancellationToken cancellationToken)
	{
		var photo = _mapper.Map<PhotoEntity>(model);

		await _repository.AddAsync(photo, cancellationToken);
	}

	public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		var photo = await _repository.GetByIdAsync(id, cancellationToken);

		if (photo == null)
		{
			throw new ProfileServiceException("Photo with this id does not exist");
		}

		await _repository.RemoveAsync(photo, cancellationToken);
	}

	public async Task<IEnumerable<PhotoModel>> GetAllAsync(CancellationToken cancellationToken)
	{
		var photos = await _repository.GetAllAsync(cancellationToken);

		var listOfPhotos = new List<PhotoModel>();

		foreach (var photo in photos)
		{
			var photoModel = _mapper.Map<PhotoModel>(photo);
			listOfPhotos.Add(photoModel);
		}

		return listOfPhotos;
	}

	public async Task<PhotoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var photo = await _repository.GetByIdAsync(id, cancellationToken);

		if (photo == null)
		{
			throw new ProfileServiceException("Photo with this id does not exist");
		}

		var photoModel = _mapper.Map<PhotoModel>(photo);

		return photoModel;
	}

	public async Task UpdateAsync(PhotoModel model, CancellationToken cancellationToken)
	{
		var photo = _mapper.Map<PhotoEntity>(model);

		await _repository.UpdateAsync(photo, cancellationToken);
	}
}
