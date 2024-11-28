﻿namespace Meetme.ProfileService.BLL.Models.PreferenceModels;

public class CreatePreferenceModel
{
	public Guid ProfileId { get; set; }
	public int MinAge { get; set; }
	public int MaxAge { get; set; }
	public string? GenderPreference { get; set; }
	public int DistanceRadius { get; set; }
}
