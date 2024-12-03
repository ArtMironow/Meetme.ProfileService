using MapsterMapper;
using Meetme.ProfileService.API.Common.Routes;
using Meetme.ProfileService.API.ViewModels.ProfileViewModels;
using Meetme.ProfileService.BLL.Interfaces;
using Meetme.ProfileService.BLL.Models.ProfileModels;
using Microsoft.AspNetCore.Mvc;

namespace Meetme.ProfileService.API.Controllers;

[Route(BaseRoutes.Profiles)]
[ApiController]
public class ProfilesController : ControllerBase
{
	private readonly IGenericService<ProfileModel, CreateProfileModel, UpdateProfileModel> _profileService;
	private readonly IMapper _mapper;

	public ProfilesController(IGenericService<ProfileModel, CreateProfileModel, UpdateProfileModel> profileService, IMapper mapper)
	{
		_profileService = profileService;
		_mapper = mapper;
	}

	[HttpPost]
	public async Task CreateAsync(CreateProfileViewModel viewModel, CancellationToken cancellationToken)
	{
		var model = _mapper.Map<CreateProfileModel>(viewModel);

		await _profileService.AddAsync(model, cancellationToken);
	}

	[HttpDelete("{id}")]
	public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		await _profileService.DeleteAsync(id, cancellationToken);
	}

	[HttpPut("{id}")]
	public async Task UpdateAsync(Guid id,UpdateProfileViewModel viewModel, CancellationToken cancellationToken)
	{
		var model = _mapper.Map<UpdateProfileModel>(viewModel);

		await _profileService.UpdateAsync(id, model, cancellationToken);
	}

	[HttpGet]
	public async Task<IEnumerable<ProfileViewModel>> GetAllAsync(CancellationToken cancellationToken)
	{
		var profileModels = await _profileService.GetAllAsync(cancellationToken);

		var profileViewModels = _mapper.Map<IEnumerable<ProfileViewModel>>(profileModels);

		return profileViewModels;
	}

	[HttpGet("{id}")]
	public async Task<ProfileViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var profileModel = await _profileService.GetByIdAsync(id, cancellationToken);

		var profileViewModel = _mapper.Map<ProfileViewModel>(profileModel);

		return profileViewModel;
	}
}
