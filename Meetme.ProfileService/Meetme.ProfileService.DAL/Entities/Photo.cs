namespace Meetme.ProfileService.DAL.Entities;

public class Photo : BaseEntity 
{
	public Guid ProfileId { get; set; }
	public string PhotoUrl { get; set; }
	public bool IsProfilePicture { get; set; }
	public DateTime UploadedAt { get; set; }
	public Profile Profile { get; set; }

}
