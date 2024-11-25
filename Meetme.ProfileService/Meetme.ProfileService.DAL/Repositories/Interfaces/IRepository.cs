using Meetme.ProfileService.DAL.Entities;

namespace Meetme.ProfileService.DAL.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
	Task AddAsync(TEntity entity, CancellationToken cancellationToken);
	Task RemoveAsync(TEntity entity, CancellationToken cancellationToken);
	Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
}