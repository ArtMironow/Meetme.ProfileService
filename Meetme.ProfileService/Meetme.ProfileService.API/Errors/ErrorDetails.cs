using System.Net;

namespace Meetme.ProfileService.API.Errors;

public class ErrorDetails
{
	public HttpStatusCode StatusCode { get; set; }
	public string? ErrorTitle { get; set; }
	public string? ErrorMessage { get; set; }
}
