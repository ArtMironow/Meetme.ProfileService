using Meetme.ProfileService.DAL.Data;
using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetme.ProfileService.DAL.Repositories;

internal class ProfileRepository : Repository<Profile>, IProfileRepository
{
	public ProfileRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<Profile?> GetByIdAsync(Guid profileId)
	{
		var profile = await DbContext.Profiles.Include(p => p.Preference).Include(p => p.Photos).SingleOrDefaultAsync(p => p.Id == profileId);

		return profile;
	}

	public async Task<IEnumerable<Profile>> GetAllAsync()
	{
		return await DbContext.Profiles.Include(p => p.Preference).Include(p => p.Photos).ToListAsync();
	}
}
