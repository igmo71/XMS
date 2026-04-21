using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace XMS.Application.Endpoints;

public class ApiKeyAuthFilter(ILogger<ApiKeyAuthFilter> logger, IConfiguration configuration) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var httpContext = context.HttpContext;

        if (!httpContext.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey))
        {
            if (logger.IsEnabled(LogLevel.Warning))
                logger.LogWarning("{Source} X-Api-Key missing", nameof(ApiKeyAuthFilter));
            return Results.Problem(detail: "X-Api-Key missing", title: "Unauthorized", statusCode: 401);
        }

        var clientName = configuration.GetSection("ApiKeys")[extractedApiKey.ToString()];

        if (string.IsNullOrEmpty(clientName))
        {
            if (logger.IsEnabled(LogLevel.Warning))
                logger.LogWarning("{Source} Access attempt with invalid key {ApiKey}", nameof(ApiKeyAuthFilter), extractedApiKey.ToString());
            return Results.Problem(detail: "Access attempt with invalid key", title: "Unauthorized", statusCode: 401);
        }

        httpContext.Items["ClientName"] = clientName;

        return await next(context);
    }
}
