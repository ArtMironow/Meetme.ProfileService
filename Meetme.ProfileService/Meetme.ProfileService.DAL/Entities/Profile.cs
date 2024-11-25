using Meetme.ProfileService.DAL.Entities.Enums;
using Meetme.ProfileService.DAL.Entities.Interfaces;

namespace Meetme.ProfileService.DAL.Entities;

public class Profile : BaseEntity, ITimestamped
{
	public Guid IdentityId { get; set; }
	public string Name { get; set; }
	public int Age { get; set; }
	public string Bio { get; set; }
	public Gender Gender {  get; set; }
	public string Location { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public Preference Preference { get; set; }
	public ICollection<Photo>? Photos { get; set; }

}
