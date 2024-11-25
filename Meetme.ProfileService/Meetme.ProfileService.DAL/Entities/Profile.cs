using Meetme.ProfileService.DAL.Entities.Enums;

namespace Meetme.ProfileService.DAL.Entities;

public class Profile : BaseEntity
{
	public Guid IdentityId { get; set; }
	public string Name { get; set; }
	public int Age { get; set; }
	public string Bio { get; set; }
	public Gender Gender {  get; set; }
	public string Location { get; set; }
	public Preference Preference { get; set; }
	public ICollection<Photo>? Photos { get; set; }
}
