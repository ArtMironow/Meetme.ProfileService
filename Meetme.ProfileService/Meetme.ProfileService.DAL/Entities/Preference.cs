using Meetme.ProfileService.DAL.Entities.Enums;
using Meetme.ProfileService.DAL.Entities.Interfaces;

namespace Meetme.ProfileService.DAL.Entities;

public class Preference : BaseEntity, ITimestamped
{
	public Guid ProfileId { get; set; }
	public int MinAge { get; set; }
	public int MaxAge { get; set; }
	public Gender GenderPreference { get; set; }
	public int DistanceRadius { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public Profile Profile { get; set; }

}
