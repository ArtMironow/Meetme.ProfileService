using Meetme.ProfileService.DAL.Entities.Enums;

namespace Meetme.ProfileService.API.ViewModels.PreferenceViewModels;

public class UpdatePreferenceViewModel
{
	public int MinAge { get; set; }
	public int MaxAge { get; set; }
	public Gender GenderPreference { get; set; }
	public int DistanceRadius { get; set; }
}
