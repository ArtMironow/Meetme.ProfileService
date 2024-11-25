using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meetme.ProfileService.DAL.Configurations;

public class ProfileConfigurations : IEntityTypeConfiguration<Profile>
{
	public void Configure(EntityTypeBuilder<Profile> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Id).ValueGeneratedNever();

		builder.Property(p =>p.Name).HasMaxLength(100);
		builder.Property(p => p.Bio).HasMaxLength(500);

		builder.Property(p => p.Gender)
			   .HasConversion(
					g => g.ToString(),
					g => (Gender)Enum.Parse(typeof(Gender), g))
			   .HasMaxLength(20);

		builder.Property(p => p.Location).HasMaxLength(50);

		builder.HasOne(p => p.Preference)
			   .WithOne(p => p.Profile)
			   .HasForeignKey<Preference>(p => p.ProfileId)
			   .OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(p => p.Photos)
			   .WithOne(p => p.Profile)
			   .HasForeignKey(p => p.ProfileId)
			   .OnDelete(DeleteBehavior.Cascade);
	}
}
