using Meetme.ProfileService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meetme.ProfileService.DAL.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	public DbSet<Profile> Profiles { get; set; } = null!;
	public DbSet<Preference> Preferences { get; set; } = null!;
	public DbSet<Photo> Photos { get; set; } = null!;
}
