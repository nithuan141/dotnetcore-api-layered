using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;

namespace ApiBase.Middlewares
{
    /// <summary>
    /// Application builder extension class for configuring global exception handler.
    /// </summary>
    public static class ExceptionConfiguration
    {
        /// <summary>
        /// The extension method to configure the global excpetion handling middleware.
        /// </summary>
        /// <param name="app">Application builder object.</param>
        /// <param name="logger">The logger object.</param>
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var status = (int)HttpStatusCode.InternalServerError;
                        var message = "Internal Server Error.";
                        // If the thrown exception is unauthorized access setting the http status code to unauthorized.
                        if (contextFeature.Error.GetType() == typeof(UnauthorizedAccessException))
                        {
                            status = (int)HttpStatusCode.Unauthorized;
                            message = "Unauthorized.";
                        }
                        // Logging the exception details.
                        logger.LogError(contextFeature.Error, "An error occured");
                        // Returning the response with inernal server error status code.
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            StatusCode = status,
                            Message = message,
                            ErrorInfo = contextFeature.Error?.Message,
                        }));
                    }
                });
            });
        }
    }
}
