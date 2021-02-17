using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace ApiBase.Middlewares
{
    /// <summary>
    /// Servicecollection extension for swagger middleware configuration.
    /// </summary>
    public static class SwaggerConfigurations
    {
        /// <summary>
        /// Configure Swagger documentation for API.
        /// </summary>
        /// <param name="services">The service collection object.</param>
        public static void ConfigureSwagger(this IServiceCollection services, string apiName, string apiDescription)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", SwaggerConfigurations.ApiInfo("V1", apiName, apiDescription));
                c.AddSecurityDefinition("Bearer", SwaggerConfigurations.SecurityScheme());
                c.AddSecurityRequirement(SwaggerConfigurations.ApiSecurityRequirement());
            });
        }

        /// <summary>
        /// The extension method to configure swagger documentation middleware.
        /// </summary>
        /// <param name="app">Application builder object.</param>
        public static void UseSwaggerDocumentation(this IApplicationBuilder app, string apiName)
        {
            // Enabling the swagger documentation
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", apiName);
                c.RoutePrefix = string.Empty;
            });
        }

        /// <summary>
        /// Get the Api info object.
        /// </summary>
        /// <returns>ApiInfo object.</returns>
        private static OpenApiInfo ApiInfo(string version, string apiName, string apiDescription)
        {
            return new OpenApiInfo
            {
                Title = apiName,
                Version = version,
                Description = apiDescription,
            };
        }

        /// <summary>
        /// Gets the security scheme object.
        /// </summary>
        /// <returns>OpenApiSecurityScheme object.</returns>
        private static OpenApiSecurityScheme SecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter into field the word 'Bearer' following by space and JWT",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
            };
        }

        /// <summary>
        /// gets the security requirement object.
        /// </summary>
        /// <returns>OpenApiSecurityRequirement object.</returns>
        private static OpenApiSecurityRequirement ApiSecurityRequirement()
        {
            return new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        new List<string>()
                    },
                };
        }
    }
}
