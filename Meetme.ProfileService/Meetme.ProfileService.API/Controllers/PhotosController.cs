using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models.PhotoModels;
using Microsoft.AspNetCore.Mvc;

namespace Meetme.ProfileService.API.Controllers;

[Route("profiles/{profileId}/photos")]
[ApiController]
public class PhotosController : ControllerBase
{
	private readonly IGenericService<PhotoModel, CreatePhotoModel, UpdatePhotoModel> _photoService;

	public PhotosController(IGenericService<PhotoModel, CreatePhotoModel, UpdatePhotoModel> photoService)
	{
		_photoService = photoService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateAsync(CreatePhotoModel model, CancellationToken cancellationToken)
	{
		await _photoService.AddAsync(model, cancellationToken);

		return Ok();
	}

	[HttpPatch("{id}")]
	public async Task<IActionResult> UpdateAsync(Guid id, UpdatePhotoModel model, CancellationToken cancellationToken)
	{
		await _photoService.UpdateAsync(id, model, cancellationToken);

		return Ok();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		await _photoService.DeleteAsync(id, cancellationToken);

		return Ok();
	}
}
