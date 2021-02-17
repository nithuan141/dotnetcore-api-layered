using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiBase.Middlewares
{
    /// <summary>
    /// ServiceCollection extension clas for logger.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Extension method to configure the logger.
        /// </summary>
        /// <param name="services">The service collection object.</param>
        /// <param name="configuration">The configuration object.</param>
        public static void ConfigureLogger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(options =>
            {
                options.AddConsole();
                options.SetMinimumLevel(LogLevel.Trace);

                // Configuring the application insight provider for the logger.
                options.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Trace);

                var instrumentationKey = configuration.GetSection("ApplicationInsights:InstrumentationKey").Value;
                options.AddApplicationInsights(instrumentationKey);
            });


        }
    }
}
