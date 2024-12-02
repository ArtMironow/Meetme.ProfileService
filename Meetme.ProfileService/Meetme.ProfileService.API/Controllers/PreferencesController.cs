using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models.PreferenceModels;
using Microsoft.AspNetCore.Mvc;

namespace Meetme.ProfileService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PreferencesController : ControllerBase
{
	private readonly IGenericService<PreferenceModel, CreatePreferenceModel, UpdatePreferenceModel> _preferenceService;

	public PreferencesController(IGenericService<PreferenceModel, CreatePreferenceModel, UpdatePreferenceModel> preferenceService)
	{
		_preferenceService = preferenceService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateAsync(CreatePreferenceModel model, CancellationToken cancellationToken)
	{
		await _preferenceService.AddAsync(model, cancellationToken);

		return Ok();
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateAsync(Guid id, UpdatePreferenceModel model, CancellationToken cancellationToken)
	{
		await _preferenceService.UpdateAsync(id, model, cancellationToken);

		return Ok();
	}
}
