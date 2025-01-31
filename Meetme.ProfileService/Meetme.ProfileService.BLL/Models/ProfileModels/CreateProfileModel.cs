using Meetme.ProfileService.BLL.Models.PhotoModels;
using Meetme.ProfileService.BLL.Models.PreferenceModels;
using Meetme.ProfileService.DAL.Entities.Enums;

namespace Meetme.ProfileService.BLL.Models.ProfileModels;

public class CreateProfileModel
{
	public required string IdentityId { get; set; }
	public string? Name { get; set; }
	public int Age { get; set; }
	public string? Bio { get; set; }
	public Gender Gender { get; set; }
	public string? Location { get; set; }
	public CreatePreferenceModel? Preference { get; set; }
	public ICollection<CreatePhotoModel>? Photos { get; set; }
}
