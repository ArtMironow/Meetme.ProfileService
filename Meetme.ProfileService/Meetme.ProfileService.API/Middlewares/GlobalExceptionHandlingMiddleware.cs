using Meetme.ProfileService.API.Errors;
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
		catch (Exception ex)
		{
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			var errorDetails = new ErrorDetails
			{
				StatusCode = HttpStatusCode.InternalServerError,
				ErrorTitle = "Server error",
				ErrorMessage = ex.Message
			};

			var responseJson = JsonSerializer.Serialize(errorDetails);

			await context.Response.WriteAsJsonAsync(responseJson);
		}
	}
}

