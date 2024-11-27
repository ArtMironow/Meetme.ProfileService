﻿using Meetme.ProfileService.DAL.Entities;

namespace Meetme.ProfileService.BLL.Models;

public class PhotoModel
{
	public Guid Guid { get; set; }
	public Guid ProfileId { get; set; }
	public string? PhotoUrl { get; set; }
	public bool IsProfilePicture { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public ProfileModel? Profile { get; set; }
}