using Meetme.ProfileService.DAL.Data;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetme.ProfileService.DAL.Repositories;

public class ProfileRepository : Repository<ProfileEntity>, IRepository<ProfileEntity>
{
	public ProfileRepository(ApplicationDbContext context) : base(context)
	{
	}

	public override async Task<ProfileEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var profile = await DbContext.Profiles
			.Include(p => p.Preference)
			.Include(p => p.Photos)
			.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

		return profile;
	}

	public override async Task<IEnumerable<ProfileEntity>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await DbContext.Profiles
			.Include(p => p.Preference)
			.Include(p => p.Photos)
			.ToListAsync(cancellationToken);
	}
}
