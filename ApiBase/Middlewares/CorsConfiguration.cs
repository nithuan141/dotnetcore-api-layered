using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBase.Middlewares
{
    /// <summary>
    /// Service collection extension for CORS configuration.
    /// </summary>
    public static class CorsConfiguration
    {
        /// <summary>
        /// Configuring the allowed origing by reading the list of origins from app settings 
        /// </summary>
        /// <param name="services">The service collection object.</param>
        /// <param name="policy">The name of the origin policy.</param>
        /// <param name="configuration">Configuration object.</param>
        public static void ConfigureCors(this IServiceCollection services, string policy, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    policy,
                    builder =>
                    {
                        var corsOrigins = configuration.GetSection("Cors:Origins").Get<string[]>();
                        builder.WithOrigins(corsOrigins).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                    });
            });
        }
    }
}
