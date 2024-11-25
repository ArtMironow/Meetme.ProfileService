using Meetme.ProfileService.DAL.Entities;

namespace Meetme.ProfileService.DAL.Repositories.Interfaces;

public interface IProfileRepository
{
	Task<IEnumerable<Profile>> GetAllAsync(CancellationToken cancellationToken);
	Task<Profile?> GetByIdAsync(Guid profileId, CancellationToken cancellationToken);
}