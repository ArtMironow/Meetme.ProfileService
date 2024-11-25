using Meetme.ProfileService.DAL.Entities;

namespace Meetme.ProfileService.DAL.Repositories.Interfaces;

internal interface IRepository<TEntity> where TEntity : BaseEntity
{
	Task AddAsync(TEntity entity);
	Task RemoveAsync(TEntity entity);
	Task UpdateAsync(TEntity entity);
}