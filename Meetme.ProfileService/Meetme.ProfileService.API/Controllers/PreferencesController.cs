using MapsterMapper;
using Meetme.ProfileService.API.Common.Routes;
using Meetme.ProfileService.API.ViewModels.PreferenceViewModels;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models.PreferenceModels;
using Microsoft.AspNetCore.Mvc;

namespace Meetme.ProfileService.API.Controllers;

[Route(BaseRoutes.Preferences)]
[ApiController]
public class PreferencesController : ControllerBase
{
	private readonly IGenericService<PreferenceModel, CreatePreferenceModel, UpdatePreferenceModel> _preferenceService;
	private readonly IMapper _mapper;

	public PreferencesController(IGenericService<PreferenceModel, CreatePreferenceModel, UpdatePreferenceModel> preferenceService, IMapper mapper)
	{
		_preferenceService = preferenceService;
		_mapper = mapper;
	}

	[HttpPost]
	public async Task CreateAsync(CreatePreferenceViewModel viewModel, CancellationToken cancellationToken)
	{
		var model = _mapper.Map<CreatePreferenceModel>(viewModel);

		await _preferenceService.AddAsync(model, cancellationToken);
	}

	[HttpPut("{id}")]
	public async Task UpdateAsync(Guid id, UpdatePreferenceViewModel viewModel, CancellationToken cancellationToken)
	{
		var model = _mapper.Map<UpdatePreferenceModel>(viewModel);

		await _preferenceService.UpdateAsync(id, model, cancellationToken);
	}
}
