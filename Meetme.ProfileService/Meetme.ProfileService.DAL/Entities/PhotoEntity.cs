namespace Meetme.ProfileService.DAL.Entities;

public class PhotoEntity : BaseEntity
{
	public Guid ProfileId { get; set; }
	public string? PhotoUrl { get; set; }
	public bool IsProfilePicture { get; set; }
	public ProfileEntity? Profile { get; set; }
}
