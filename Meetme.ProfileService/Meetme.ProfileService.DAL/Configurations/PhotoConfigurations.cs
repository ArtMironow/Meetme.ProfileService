﻿using Meetme.ProfileService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meetme.ProfileService.DAL.Configurations;

public class PhotoConfigurations : IEntityTypeConfiguration<Photo>
{
	public void Configure(EntityTypeBuilder<Photo> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id).ValueGeneratedNever();

		builder.Property(p => p.PhotoUrl).HasMaxLength(500);

		builder.Property(p => p.IsProfilePicture)
			   .HasDefaultValue(false);

		builder.HasOne(p => p.Profile)
			   .WithMany(p => p.Photos)
			   .HasForeignKey(p => p.ProfileId);
	}
}
