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

	public Profile(Guid id, Guid identityId, string name, int age, string bio, Gender gender, string location) : base(id)
	{
		IdentityId = identityId;
		Name = name;
		Age = age;
		Bio = bio;
		Gender = gender;
		Location = location;
	}
}
