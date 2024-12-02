using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models.ProfileModels;
using Microsoft.AspNetCore.Mvc;

namespace Meetme.ProfileService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfilesController : ControllerBase
{
	private readonly IGenericService<ProfileModel, CreateProfileModel, UpdateProfileModel> _profileService;

	public ProfilesController(IGenericService<ProfileModel, CreateProfileModel, UpdateProfileModel> profileService)
	{
		_profileService = profileService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateAsync(CreateProfileModel model, CancellationToken cancellationToken)
	{
		await _profileService.AddAsync(model, cancellationToken);

		return Ok();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		await _profileService.DeleteAsync(id, cancellationToken);

		return Ok();
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateAsync(Guid id,UpdateProfileModel model, CancellationToken cancellationToken)
	{
		await _profileService.UpdateAsync(id, model, cancellationToken);

		return Ok();
	}

	[HttpGet]
	public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
	{
		var profiles = await _profileService.GetAllAsync(cancellationToken);

		return Ok(profiles);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var profile = await _profileService.GetByIdAsync(id, cancellationToken);

		return Ok(profile);
	}
}
