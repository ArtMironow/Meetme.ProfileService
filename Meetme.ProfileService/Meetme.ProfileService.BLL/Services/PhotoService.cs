using MapsterMapper;
using Meetme.ProfileService.BLL.Exceptions;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models;
using Meetme.ProfileService.BLL.Models.PhotoModels;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Repositories.Interfaces;

namespace Meetme.ProfileService.BLL.Services;

public class PhotoService : IServiceOperations<PhotoModel, CreatePhotoModel, UpdatePhotoModel>
{
	private readonly IRepository<PhotoEntity> _repository;
	private readonly IMapper _mapper;

	public PhotoService(IRepository<PhotoEntity> repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task AddAsync(CreatePhotoModel model, CancellationToken cancellationToken)
	{
		var photo = _mapper.Map<PhotoEntity>(model);

		await _repository.AddAsync(photo, cancellationToken);
	}

	public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		var photo = await _repository.GetByIdAsync(id, cancellationToken);

		if (photo == null)
		{
			throw new BusinessLogicException("Photo with this id does not exist");
		}

		await _repository.RemoveAsync(photo, cancellationToken);
	}

	public async Task<IEnumerable<PhotoModel>> GetAllAsync(CancellationToken cancellationToken)
	{
		var photos = await _repository.GetAllAsync(cancellationToken);

		var listOfPhotos = _mapper.Map<IEnumerable<PhotoModel>>(photos);

		return listOfPhotos;
	}

	public async Task<PhotoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var photo = await _repository.GetByIdAsync(id, cancellationToken);

		if (photo == null)
		{
			throw new BusinessLogicException("Photo with this id does not exist");
		}

		var photoModel = _mapper.Map<PhotoModel>(photo);

		return photoModel;
	}

	public async Task UpdateAsync(Guid id, UpdatePhotoModel model, CancellationToken cancellationToken)
	{
		var photo = await _repository.GetByIdAsync(id, cancellationToken);

		if (photo == null)
		{
			throw new BusinessLogicException("Photo does not exist");
		}

		photo.IsProfilePicture = model.IsProfilePicture;

		await _repository.UpdateAsync(photo, cancellationToken);
	}
}
