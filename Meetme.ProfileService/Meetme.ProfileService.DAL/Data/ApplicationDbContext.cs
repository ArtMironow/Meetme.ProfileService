using Meetme.ProfileService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meetme.ProfileService.DAL.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
		Database.EnsureCreated();
	}

	public DbSet<ProfileEntity>? Profiles { get; set; }
	public DbSet<PreferenceEntity>? Preferences { get; set; }
	public DbSet<PhotoEntity>? Photos { get; set; }
}
