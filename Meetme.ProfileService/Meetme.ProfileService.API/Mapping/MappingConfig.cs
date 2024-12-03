using Mapster;
using Meetme.ProfileService.API.ViewModels.PhotoViewModels;
using Meetme.ProfileService.API.ViewModels.PreferenceViewModels;
using Meetme.ProfileService.API.ViewModels.ProfileViewModels;
using Meetme.ProfileService.BLL.Models.PhotoModels;
using Meetme.ProfileService.BLL.Models.PreferenceModels;
using Meetme.ProfileService.BLL.Models.ProfileModels;

namespace Meetme.ProfileService.API.Mapping;

public class MappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<PhotoViewModel, PhotoModel>();
		config.NewConfig<CreatePhotoViewModel, CreatePhotoModel>();
		config.NewConfig<UpdatePhotoViewModel, UpdatePhotoModel>();

		config.NewConfig<PreferenceViewModel, PreferenceModel>();
		config.NewConfig<CreatePreferenceViewModel, CreatePreferenceModel>();
		config.NewConfig<UpdatePreferenceViewModel, UpdatePreferenceModel>();

		config.NewConfig<ProfileViewModel, ProfileModel>();
		config.NewConfig<CreateProfileViewModel, CreateProfileModel>();
		config.NewConfig<UpdateProfileViewModel, UpdateProfileModel>();
	}
}
