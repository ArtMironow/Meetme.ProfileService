using Meetme.ProfileService.DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Meetme.ProfileService.DAL.Data;

public sealed class TimestampInterceptor : SaveChangesInterceptor
{
	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
		DbContextEventData eventData,
		InterceptionResult<int> result,
		CancellationToken cancellationToken = default)
	{
		UpdateTimestamps(eventData.Context);

		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}

	private static void UpdateTimestamps(DbContext? context)
	{
		if (context == null) return;

		foreach (var entry in context.ChangeTracker.Entries())
		{
			if (entry.Entity is ITimestamped entity)
			{
				var utcNow = DateTime.UtcNow;

				if (entry.State == EntityState.Added)
				{
					entity.CreatedAt = utcNow;
					entity.UpdatedAt = utcNow;
				}
				else if (entry.State == EntityState.Modified)
				{
					entity.UpdatedAt = utcNow;
				}
			}
		}
	}
}
