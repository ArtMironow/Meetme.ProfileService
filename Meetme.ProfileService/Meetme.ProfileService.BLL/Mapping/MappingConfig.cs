using Mapster;
using Meetme.ProfileService.BLL.Models;
using Meetme.ProfileService.BLL.Models.PhotoModels;
using Meetme.ProfileService.BLL.Models.PreferenceModels;
using Meetme.ProfileService.BLL.Models.ProfileModels;
using Meetme.ProfileService.DAL.Entities;

namespace Meetme.ProfileService.BLL.Mapping;

public class MappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<PhotoModel, PhotoEntity>();
		config.NewConfig<CreatePhotoModel, PhotoEntity>();
		config.NewConfig<UpdatePhotoModel, PhotoEntity>();

		config.NewConfig<PreferenceModel, PreferenceEntity>();
		config.NewConfig<CreatePreferenceModel, PreferenceEntity>();
		config.NewConfig<UpdatePreferenceModel, PreferenceEntity>();

		config.NewConfig<ProfileModel, ProfileEntity>();
		config.NewConfig<CreateProfileModel, ProfileEntity>();
		config.NewConfig<UpdateProfileModel, ProfileEntity>();
	}
}
