using MapsterMapper;
using Meetme.ProfileService.API.Common.Routes;
using Meetme.ProfileService.API.ViewModels.PhotoViewModels;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models.PhotoModels;
using Microsoft.AspNetCore.Mvc;

namespace Meetme.ProfileService.API.Controllers;

[Route(BaseRoutes.Photos)]
[ApiController]
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
	public async Task CreateAsync(CreatePhotoViewModel viewModel, CancellationToken cancellationToken)
	{
		var model = _mapper.Map<CreatePhotoModel>(viewModel);

		await _photoService.AddAsync(model, cancellationToken);
	}

	[HttpPatch("{id}")]
	public async Task UpdateAsync(Guid id, UpdatePhotoViewModel viewModel, CancellationToken cancellationToken)
	{
		var model = _mapper.Map<UpdatePhotoModel>(viewModel);

		await _photoService.UpdateAsync(id, model, cancellationToken);
	}

	[HttpDelete("{id}")]
	public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		await _photoService.DeleteAsync(id, cancellationToken);
	}
}
