﻿namespace Meetme.ProfileService.BLL.Models.PhotoModels;

public class CreatePhotoModel
{
	public Guid ProfileId { get; set; }
	public string? PhotoUrl { get; set; }
	public bool IsProfilePicture { get; set; }
}
