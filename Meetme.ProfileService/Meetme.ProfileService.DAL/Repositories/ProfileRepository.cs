using Meetme.ProfileService.DAL.Data;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetme.ProfileService.DAL.Repositories;

public class ProfileRepository : Repository<Profile>, IProfileRepository
{
	public ProfileRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<Profile?> GetByIdAsync(Guid profileId, CancellationToken cancellationToken)
	{
		var profile = await DbContext.Profiles
			.Include(p => p.Preference)
			.Include(p => p.Photos)
			.SingleOrDefaultAsync(p => p.Id == profileId, cancellationToken);

		return profile;
	}

	public async Task<IEnumerable<Profile>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await DbContext.Profiles
			.Include(p => p.Preference)
			.Include(p => p.Photos)
			.ToListAsync(cancellationToken);
	}
}
