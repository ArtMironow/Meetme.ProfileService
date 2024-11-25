namespace Meetme.ProfileService.DAL.Entities.Interfaces;

public interface ITimestamped
{
	DateTime CreatedAt { get; set; }
	DateTime UpdatedAt { get; set; }
}
