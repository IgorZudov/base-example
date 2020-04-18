using System;
using CodeJam;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ziv.CodeExample.Helpers;

namespace Ziv.CodeExample.Web
{
    public static class ExceptionHandlerMiddleware
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            return app.Use(async (context, func) =>
            {
                try
                {
                    await func();
                }
                catch (GuardArgumentException e)
                {
                    logger.LogWarning(e.ToDiagnosticString());
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync(e.Message);
                }
                catch (Exception e)
                {
                    logger.LogError(e.ToDiagnosticString());
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(e.Message);
                }
            });
        }
    }
}