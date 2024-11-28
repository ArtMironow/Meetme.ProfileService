using Mapster;
using Meetme.ProfileService.BLL.Models;
using Meetme.ProfileService.BLL.Models.PhotoModels;
using Meetme.ProfileService.BLL.Models.PreferenceModels;
using Meetme.ProfileService.BLL.Models.ProfileModels;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Entities.Enums;

namespace Meetme.ProfileService.BLL.Mapping;

public class MappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<PhotoModel, PhotoEntity>();
		config.NewConfig<CreatePhotoModel, PhotoEntity>()
			.Map(dest => dest.Id, src => Guid.NewGuid());

		config.NewConfig<PreferenceModel, PreferenceEntity>();
		config.NewConfig<CreatePreferenceModel, PreferenceEntity>()
			.Map(dest => dest.Id, src => Guid.NewGuid())
			.Map(dest => dest.GenderPreference, src => Enum.Parse<Gender>(src.GenderPreference!));

		config.NewConfig<ProfileModel, ProfileEntity>();
		config.NewConfig<CreateProfileModel, ProfileEntity>()
			.Map(dest => dest.Id, src => Guid.NewGuid())
			.Map(dest => dest.Gender, src => Enum.Parse<Gender>(src.Gender!));
	}
}
