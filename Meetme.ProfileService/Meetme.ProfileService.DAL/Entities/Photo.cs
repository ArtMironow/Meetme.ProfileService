using Meetme.ProfileService.DAL.Entities.Interfaces;

namespace Meetme.ProfileService.DAL.Entities;

public class Photo : BaseEntity, ITimestamped
{
	public Guid ProfileId { get; set; }
	public string PhotoUrl { get; set; }
	public bool IsProfilePicture { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public Profile Profile { get; set; }

}
