using Meetme.ProfileService.DAL.Entities.Enums;

namespace Meetme.ProfileService.BLL.Models.ProfileModels;

public class UpdateProfileModel
{
	public string? Name { get; set; }
	public int Age { get; set; }
	public string? Bio { get; set; }
	public Gender Gender { get; set; }
	public string? Location { get; set; }
}
