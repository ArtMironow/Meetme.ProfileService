using Meetme.ProfileService.DAL.Entities;

namespace Meetme.ProfileService.DAL.Repositories.Interfaces;

public interface IProfileRepository
{
	Task<IEnumerable<ProfileEntity>> GetAllAsync(CancellationToken cancellationToken);
	Task<ProfileEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}