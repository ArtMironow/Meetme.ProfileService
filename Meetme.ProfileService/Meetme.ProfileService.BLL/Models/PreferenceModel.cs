using Meetme.ProfileService.DAL.Entities.Enums;

namespace Meetme.ProfileService.BLL.Models;

public class PreferenceModel
{
	public Guid Id { get; set; }
	public Guid ProfileId { get; set; }
	public int MinAge { get; set; }
	public int MaxAge { get; set; }
	public string? GenderPreference { get; set; }
	public int DistanceRadius { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public ProfileModel? Profile { get; set; }
}
