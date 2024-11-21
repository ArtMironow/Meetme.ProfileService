using System.Net;

namespace Meetme.ProfileService.API.Errors;

public class ErrorDetails
{
	public string? ErrorTitle { get; set; }
	public string? ErrorMessage { get; set; }
}
