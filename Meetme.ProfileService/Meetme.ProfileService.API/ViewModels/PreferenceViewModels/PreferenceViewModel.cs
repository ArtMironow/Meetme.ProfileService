using Meetme.ProfileService.DAL.Entities.Enums;

namespace Meetme.ProfileService.API.ViewModels.PreferenceViewModels;

public class PreferenceViewModel
{
	public Guid Id { get; set; }
	public Guid ProfileId { get; set; }
	public int MinAge { get; set; }
	public int MaxAge { get; set; }
	public Gender GenderPreference { get; set; }
	public int DistanceRadius { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
