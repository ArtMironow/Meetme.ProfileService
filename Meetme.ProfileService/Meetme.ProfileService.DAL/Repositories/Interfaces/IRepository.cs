using Meetme.ProfileService.DAL.Entities;

namespace Meetme.ProfileService.DAL.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
	Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
	Task AddAsync(TEntity entity, CancellationToken cancellationToken);
	Task RemoveAsync(TEntity entity, CancellationToken cancellationToken);
	Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
}