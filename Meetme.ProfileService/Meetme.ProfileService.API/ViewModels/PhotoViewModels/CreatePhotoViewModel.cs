namespace Meetme.ProfileService.API.ViewModels.PhotoViewModels;

public class CreatePhotoViewModel
{
	public Guid ProfileId { get; set; }
	public required string PhotoUrl { get; set; }
	public bool IsProfilePicture { get; set; }
}
