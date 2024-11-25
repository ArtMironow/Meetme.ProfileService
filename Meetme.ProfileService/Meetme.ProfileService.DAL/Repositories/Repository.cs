using Meetme.ProfileService.DAL.Data;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Repositories.Interfaces;

namespace Meetme.ProfileService.DAL.Repositories;

internal class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
	protected readonly ApplicationDbContext DbContext;

	protected Repository(ApplicationDbContext dbContext)
	{
		DbContext = dbContext;
	}

	public async Task AddAsync(TEntity entity)
	{
		await DbContext.Set<TEntity>().AddAsync(entity);
		await DbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(TEntity entity)
	{
		DbContext.Set<TEntity>().Update(entity);
		await DbContext.SaveChangesAsync();
	}

	public async Task RemoveAsync(TEntity entity)
	{
		DbContext.Set<TEntity>().Remove(entity);
		await DbContext.SaveChangesAsync();
	}
}
