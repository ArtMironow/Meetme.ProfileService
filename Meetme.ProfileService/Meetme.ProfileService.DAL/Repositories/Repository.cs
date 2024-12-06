using Meetme.ProfileService.DAL.Data;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Meetme.ProfileService.DAL.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
	protected readonly ApplicationDbContext DbContext;

	public Repository(ApplicationDbContext dbContext)
	{
		DbContext = dbContext;
	}

	public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return await DbContext.Set<TEntity>().FindAsync(id, cancellationToken);
	}

	public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await DbContext.Set<TEntity>().ToListAsync(cancellationToken);
	}

	public Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
	{
		return DbContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync(cancellationToken);
	}

	public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
	{
		await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
		await DbContext.SaveChangesAsync(cancellationToken);
	}

	public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
	{
		DbContext.Set<TEntity>().Update(entity);
		return DbContext.SaveChangesAsync(cancellationToken);
	}

	public Task RemoveAsync(TEntity entity, CancellationToken cancellationToken)
	{
		DbContext.Set<TEntity>().Remove(entity);
		return DbContext.SaveChangesAsync(cancellationToken);
	}
}
