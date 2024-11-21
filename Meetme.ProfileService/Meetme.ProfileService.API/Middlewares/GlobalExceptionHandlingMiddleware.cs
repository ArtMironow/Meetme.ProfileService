using Microsoft.AspNetCore.Mvc;
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
		catch (Exception)
		{
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			ProblemDetails errorDetails = new()
			{
				Status = (int)HttpStatusCode.InternalServerError,
				Title = "Server error",
				Detail = "An internal server error occured"
			};

			var responseJson = JsonSerializer.Serialize(errorDetails);

			await context.Response.WriteAsJsonAsync(responseJson);
		}
	}
}

