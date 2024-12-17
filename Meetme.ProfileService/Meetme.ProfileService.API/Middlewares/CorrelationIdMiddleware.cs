using Meetme.ProfileService.API.Common.Headers;

namespace Meetme.ProfileService.API.Middlewares;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext context)
    {
        var hasCorrelationId = context.Request.Headers.TryGetValue(HeaderKeys.CorrelationIdHeaderName, out var correlationId);

        if (!hasCorrelationId)
        {
            correlationId = Guid.NewGuid().ToString();
        }

        context.TraceIdentifier = correlationId!;
        context.Response.Headers.Append(HeaderKeys.CorrelationIdHeaderName, correlationId);

        return _next(context);
    }
}

