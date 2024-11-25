using Meetme.ProfileService.DAL.Entities;

namespace Meetme.ProfileService.DAL.Repositories.Interfaces
{
	internal interface IProfileRepository
	{
		Task<IEnumerable<Profile>> GetAllAsync();
		Task<Profile?> GetByIdAsync(Guid profileId);
	}
}