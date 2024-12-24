using MapsterMapper;
using Meetme.ProfileService.BLL.Exceptions;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models.PhotoModels;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Meetme.ProfileService.BLL.Services;

public class PhotoService : IGenericService<PhotoModel, CreatePhotoModel, UpdatePhotoModel>
{
	private readonly IRepository<PhotoEntity> _repository;
	private readonly IMapper _mapper;
	private readonly ILogger<PhotoService> _logger;

	public PhotoService(IRepository<PhotoEntity> repository, IMapper mapper, ILogger<PhotoService> logger)
	{
		_repository = repository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task AddAsync(CreatePhotoModel model, CancellationToken cancellationToken)
	{
		if (model.IsProfilePicture == true)
		{
			await UpdateProfilePicture(model.ProfileId, cancellationToken);
		}

		var photo = _mapper.Map<PhotoEntity>(model);

		await _repository.AddAsync(photo, cancellationToken);
	}

	public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		var photo = await _repository.GetByIdAsync(id, cancellationToken);

		if (photo == null)
		{
			_logger.LogWarning("Photo with ID = {Id} was not found", id);
			throw new EntityNotFoundException("Photo with this id does not exist");
		}

		await _repository.RemoveAsync(photo, cancellationToken);
	}

	public async Task<IEnumerable<PhotoModel>> GetAllAsync(CancellationToken cancellationToken)
	{
		var photoEntities = await _repository.GetAllAsync(cancellationToken);

		var photos = _mapper.Map<IEnumerable<PhotoModel>>(photoEntities);

		return photos;
	}

	public async Task<PhotoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var photo = await _repository.GetByIdAsync(id, cancellationToken);

		if (photo == null)
		{
			_logger.LogWarning("Photo with ID = {Id} was not found", id);
			throw new EntityNotFoundException("Photo with this id does not exist");
		}

		var photoModel = _mapper.Map<PhotoModel>(photo);

		return photoModel;
	}

	public async Task UpdateAsync(Guid id, UpdatePhotoModel model, CancellationToken cancellationToken)
	{
		var photo = await _repository.GetByIdAsync(id, cancellationToken);

		if (photo == null)
		{
			_logger.LogWarning("Photo with ID = {Id} was not found", id);
			throw new EntityNotFoundException("Photo does not exist");
		}

		if (model.IsProfilePicture == true)
		{
			await UpdateProfilePicture(photo.ProfileId, cancellationToken);
		}
		
		_mapper.Map(model, photo);

		await _repository.UpdateAsync(photo, cancellationToken);
	}

	private async Task UpdateProfilePicture(Guid id, CancellationToken cancellationToken)
	{
		var profilePicturePhoto =
			await _repository.GetFirstOrDefaultAsync(p => p.ProfileId == id && p.IsProfilePicture == true, cancellationToken);

		if (profilePicturePhoto != null)
		{
			profilePicturePhoto.IsProfilePicture = false;

			await _repository.UpdateAsync(profilePicturePhoto, cancellationToken);
		}
	}
}
