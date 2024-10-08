﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Primitives.Rule;

/// <summary>
///     TODO: complete
/// </summary>
public class DomainBusinessRuleInvalidExceptionHandler : IExceptionHandler
{
    /// <summary>
    ///     TODO: complete
    /// </summary>
    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is DomainBusinessRuleInvalidException ruleInvalidException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.WriteAsJsonAsync<ProblemDetails>(
                new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    Title = "Bad Request",
                    Detail = ruleInvalidException.Message,
                },
                cancellationToken
            );
            return ValueTask.FromResult(result: true);
        }

        return ValueTask.FromResult(result: false);
    }
}
