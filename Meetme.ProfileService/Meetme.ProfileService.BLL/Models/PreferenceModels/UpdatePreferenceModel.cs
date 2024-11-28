﻿using Meetme.ProfileService.DAL.Entities.Enums;

namespace Meetme.ProfileService.BLL.Models.PreferenceModels;

public class UpdatePreferenceModel
{
	public int MinAge { get; set; }
	public int MaxAge { get; set; }
	public Gender GenderPreference { get; set; }
	public int DistanceRadius { get; set; }
}