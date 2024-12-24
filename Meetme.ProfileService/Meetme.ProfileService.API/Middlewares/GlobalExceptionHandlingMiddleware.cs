using Meetme.ProfileService.API.Errors;
using Meetme.ProfileService.BLL.Exceptions;
using System.Net;

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
				EntityNotFoundException => HttpStatusCode.NotFound,
				_ => HttpStatusCode.InternalServerError
			};

			context.Response.StatusCode = (int)statusCode;

			var errorDetails = new ErrorDetails
			{
				ErrorTitle = "Server error",
				ErrorMessage = ex.Message
			};

			await context.Response.WriteAsJsonAsync(errorDetails);
		}
	}
}

