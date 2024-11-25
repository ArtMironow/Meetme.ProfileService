using Meetme.ProfileService.DAL.Entities.Interfaces;

namespace Meetme.ProfileService.DAL.Entities;

public abstract class BaseEntity : ITimestamped
{
	public Guid Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
