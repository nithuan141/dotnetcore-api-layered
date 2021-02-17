using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ApiBase.Middlewares
{
    /// <summary>
    /// extnesion of service collection for API Versioning.
    /// </summary>
    public static class VesrioningConfigurations
    {
        /// <summary>
        /// Extension method to configure the API Versioning.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAPIVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}
