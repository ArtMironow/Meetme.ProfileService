namespace Meetme.ProfileService.API.Common.Routes;

public static class BaseRoutes
{
	public const string Profiles = "profiles";
	public const string Preferences = "preferences";
	public const string Photos = Profiles + "/{profileId}/photos";
}
