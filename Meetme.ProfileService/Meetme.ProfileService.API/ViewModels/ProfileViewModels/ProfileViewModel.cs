using Meetme.ProfileService.API.ViewModels.PhotoViewModels;
using Meetme.ProfileService.API.ViewModels.PreferenceViewModels;
using Meetme.ProfileService.DAL.Entities.Enums;

namespace Meetme.ProfileService.API.ViewModels.ProfileViewModels;

public class ProfileViewModel
{
	public Guid Id { get; set; }
	public Guid IdentityId { get; set; }
	public string? Name { get; set; }
	public int Age { get; set; }
	public string? Bio { get; set; }
	public Gender Gender { get; set; }
	public string? Location { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public PreferenceViewModel? Preference { get; set; }
	public ICollection<PhotoViewModel>? Photos { get; set; }
}
