using Mapster;
using Meetme.ProfileService.BLL.Models;
using Meetme.ProfileService.DAL.Entities;

namespace Meetme.ProfileService.BLL.Mapping;

public class MappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<PhotoModel, PhotoEntity>();

		config.NewConfig<PreferenceModel, PreferenceEntity>();

		config.NewConfig<ProfileModel, ProfileEntity>();
	}
}
