using Meetme.ProfileService.API.ViewModels.PhotoViewModels;
using Meetme.ProfileService.API.ViewModels.PreferenceViewModels;
using Meetme.ProfileService.DAL.Entities.Enums;

namespace Meetme.ProfileService.API.ViewModels.ProfileViewModels;

public class CreateProfileViewModel
{
	public required string IdentityId { get; set; }
	public string? Name { get; set; }
	public int Age { get; set; }
	public string? Bio { get; set; }
	public Gender Gender { get; set; }
	public string? Location { get; set; }
	public CreatePreferenceViewModel? Preference { get; set; }
	public ICollection<CreatePhotoViewModel>? Photos { get; set; }
}
