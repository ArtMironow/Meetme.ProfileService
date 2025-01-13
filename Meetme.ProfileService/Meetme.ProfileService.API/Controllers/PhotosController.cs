using MapsterMapper;
using Meetme.ProfileService.API.Common.Routes;
using Meetme.ProfileService.API.ViewModels.PhotoViewModels;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models.PhotoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meetme.ProfileService.API.Controllers;

[Route(BaseRoutes.Photos)]
[ApiController]
[Authorize]
public class PhotosController : ControllerBase
{
	private readonly IGenericService<PhotoModel, CreatePhotoModel, UpdatePhotoModel> _photoService;
	private readonly IMapper _mapper;

	public PhotosController(IGenericService<PhotoModel, CreatePhotoModel, UpdatePhotoModel> photoService, IMapper mapper)
	{
		_photoService = photoService;
		_mapper = mapper;
	}

	[HttpPost]
	public Task CreateAsync(CreatePhotoViewModel viewModel, CancellationToken cancellationToken)
	{
		var model = _mapper.Map<CreatePhotoModel>(viewModel);

		return _photoService.AddAsync(model, cancellationToken);
	}

	[HttpPatch("{id}")]
	public Task UpdateAsync(Guid id, UpdatePhotoViewModel viewModel, CancellationToken cancellationToken)
	{
		var model = _mapper.Map<UpdatePhotoModel>(viewModel);

		return _photoService.UpdateAsync(id, model, cancellationToken);
	}

	[HttpDelete("{id}")]
	public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		return _photoService.DeleteAsync(id, cancellationToken);
	}
}
