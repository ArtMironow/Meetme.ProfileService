using Meetme.ProfileService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meetme.ProfileService.DAL.Configurations;

public class PhotoConfigurations : IEntityTypeConfiguration<Photo>
{
	public void Configure(EntityTypeBuilder<Photo> builder)
	{
		builder.HasOne(p => p.Profile)
			   .WithMany(p => p.Photos)
			   .HasForeignKey(p => p.ProfileId);
	}
}
