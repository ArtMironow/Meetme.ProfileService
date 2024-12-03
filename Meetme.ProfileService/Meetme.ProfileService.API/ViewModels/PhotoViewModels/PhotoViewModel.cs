namespace Meetme.ProfileService.API.ViewModels.PhotoViewModels;

public class PhotoViewModel
{
	public Guid Id { get; set; }
	public Guid ProfileId { get; set; }
	public required string PhotoUrl { get; set; }
	public bool IsProfilePicture { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
