﻿using MapsterMapper;
using Meetme.ProfileService.API.Common.Routes;
using Meetme.ProfileService.API.ViewModels.PreferenceViewModels;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models.PreferenceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meetme.ProfileService.API.Controllers;

[Route(BaseRoutes.Preferences)]
[ApiController]
[Authorize]
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
	public Task CreateAsync(CreatePreferenceViewModel viewModel, CancellationToken cancellationToken)
	{
		var model = _mapper.Map<CreatePreferenceModel>(viewModel);

		return _preferenceService.AddAsync(model, cancellationToken);
	}

	[HttpPut("{id}")]
	public Task UpdateAsync(Guid id, UpdatePreferenceViewModel viewModel, CancellationToken cancellationToken)
	{
		var model = _mapper.Map<UpdatePreferenceModel>(viewModel);

		return _preferenceService.UpdateAsync(id, model, cancellationToken);
	}
}
