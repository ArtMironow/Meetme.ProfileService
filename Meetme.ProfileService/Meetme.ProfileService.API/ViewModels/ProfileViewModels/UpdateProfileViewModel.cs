using Meetme.ProfileService.DAL.Entities.Enums;

namespace Meetme.ProfileService.API.ViewModels.ProfileViewModels;

public class UpdateProfileViewModel
{
	public string? Name { get; set; }
	public int Age { get; set; }
	public string? Bio { get; set; }
	public Gender Gender { get; set; }
	public string? Location { get; set; }
}
