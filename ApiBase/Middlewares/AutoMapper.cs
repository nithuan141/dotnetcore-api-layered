using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiBase.Middlewares
{
    /// <summary>
    /// Service collection extension class for auto mapper configuration
    /// </summary>
    public static class AutoMapper
    {
        /// <summary>
        /// Extension method for the automapper service configuration.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="mappingProfile"></param>
        public static void AddMapperConfiguration(this IServiceCollection services, Profile mappingProfile)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(mappingProfile);
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
