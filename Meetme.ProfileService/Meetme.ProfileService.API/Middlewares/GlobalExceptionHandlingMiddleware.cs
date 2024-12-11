using Meetme.ProfileService.API.Errors;
using System.Net;
using System.Text.Json;

namespace Meetme.ProfileService.API.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			var statusCode = ex switch
			{
				KeyNotFoundException => HttpStatusCode.NotFound,
				_ => HttpStatusCode.InternalServerError
			};

			context.Response.StatusCode = (int)statusCode;

			var errorDetails = new ErrorDetails
			{
				ErrorTitle = "Server error",
				ErrorMessage = ex.Message
			};

			var responseJson = JsonSerializer.Serialize(errorDetails);

			await context.Response.WriteAsJsonAsync(responseJson);
		}
	}
}

