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
		_logger.Information("Handling request: {Method} {Path}",
			context.Request.Method, context.Request.Path);

		await _next(context);

		_logger.Information("Handled request: {Method} {Path} with {StatusCode}",
			context.Request.Method, context.Request.Path, context.Response.StatusCode);
	}
}
