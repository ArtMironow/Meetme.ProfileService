using Meetme.ProfileService.DAL.Entities.Enums;

namespace Meetme.ProfileService.DAL.Entities;

public class Preference : BaseEntity
{
	public Guid ProfileId { get; set; }
	public int MinAge { get; set; }
	public int MaxAge { get; set; }
	public Gender GenderPreference { get; set; }
	public int DistanceRadius { get; set; }
	public Profile Profile { get; set; }
}
