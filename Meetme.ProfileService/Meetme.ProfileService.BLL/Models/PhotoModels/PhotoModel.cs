﻿namespace Meetme.ProfileService.BLL.Models.PhotoModels;

public class PhotoModel
{
	public Guid Id { get; set; }
	public Guid ProfileId { get; set; }
	public required string PhotoUrl { get; set; }
	public bool IsProfilePicture { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
