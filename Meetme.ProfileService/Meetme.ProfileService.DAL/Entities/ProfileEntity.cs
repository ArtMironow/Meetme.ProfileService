using Meetme.ProfileService.DAL.Entities.Enums;

namespace Meetme.ProfileService.DAL.Entities;

public class ProfileEntity : BaseEntity
{
	public Guid IdentityId { get; set; }
	public string? Name { get; set; }
	public int Age { get; set; }
	public string? Bio { get; set; }
	public Gender Gender {  get; set; }
	public string? Location { get; set; }
	public PreferenceEntity? Preference { get; set; }
	public ICollection<PhotoEntity>? Photos { get; set; }
}
