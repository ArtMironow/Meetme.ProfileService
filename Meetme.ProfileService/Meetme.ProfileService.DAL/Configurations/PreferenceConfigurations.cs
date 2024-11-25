using Meetme.ProfileService.DAL.Entities;
using Meetme.ProfileService.DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meetme.ProfileService.DAL.Configurations;

public class PreferenceConfigurations : IEntityTypeConfiguration<Preference>
{
	public void Configure(EntityTypeBuilder<Preference> builder)
	{
		builder.Property(x => x.GenderPreference)
			   .HasConversion(
					g => g.ToString(),
					g => (Gender)Enum.Parse(typeof(Gender), g))
			   .HasMaxLength(20);

		builder.HasOne(p => p.Profile)
			   .WithOne(p => p.Preference)
			   .HasForeignKey<Preference>(p => p.ProfileId);
	}
}
