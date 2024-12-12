using ILogger = Serilog.ILogger;

namespace Meetme.ProfileService.API.Middlewares;

public class LoggingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger _logger;

	public LoggingMiddleware(RequestDelegate next, ILogger logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			_logger.Information("Handling request: {Method} {Path}",
				context.Request.Method, context.Request.Path);

			await _next(context);

			var statusCode = context.Response.StatusCode;

			LogResponse(context, statusCode);
			
		}
		catch (Exception exception)
		{
			_logger.Error(exception, "Unhandled exception occurred while processing {Method} {Path}",
				context.Request.Method, context.Request.Path);
		}
	}

	private void LogResponse(HttpContext context, int statusCode)
	{
		if (statusCode >= 500)
		{
			_logger.Error("Server error with {StatusCode} status code for {Method} {Path}",
				statusCode, context.Request.Method, context.Request.Path);
		}
		else if (statusCode >= 400)
		{
			_logger.Warning("Client error with {StatusCode} status code for {Method} {Path}",
				statusCode, context.Request.Method, context.Request.Path);
		}
		else
		{
			_logger.Information("Successfully handeled request with {StatusCode} status code for {Method} {Path}",
				statusCode, context.Request.Method, context.Request.Path);
		}
	}
}
