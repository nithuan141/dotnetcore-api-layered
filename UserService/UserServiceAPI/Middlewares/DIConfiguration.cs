using AssetServiceDataProvider.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using UserServiceAPI.Common;
using UserServiceAPI.DataAccess;
using UserServiceAPI.DataAccess.Contracts;
using UserServiceAPI.Services;
using UserServiceAPI.Services.Contracts;

namespace UserServiceAPI.Middlewares
{
    /// <summary>
    /// Servicecollection extnetion for dependency injection mappings.
    /// </summary>
    public static class DIConfiguration
    {
        /// <summary>
        /// Configuring the DI mappings.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureDI(this IServiceCollection services)
        {
            ConfigureServices(services);
            ConfigureRepository(services);
        }

        /// <summary>
        /// Extention method to configure the database connection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureDB(this IServiceCollection services, IConfiguration configuration)
        {
            // TODO: Move the connection string to a key vault and read it from vault.
            var connectionString = configuration.GetSection("ConnectionString:KeyServiceDB").Value;
            services.AddDbContext<AssetServiceContext>(options => options.UseSqlServer(connectionString));
        }

        /// <summary>
        /// Configuring the DI mapping for services.
        /// </summary>
        /// <param name="services">Service collection object.</param>
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAssetUserService, AssetUserService>();
        }

        /// <summary>
        /// Configuring the DI mapping for repository.
        /// </summary>
        /// <param name="services">Service collection object.</param>
        private static void ConfigureRepository(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IAssetUserRepository, AssetUserRepository>();
        }

        /// <summary>
        /// Adding the initial data.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="context"></param>
        public static void SetUpInitialData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AssetServiceContext>();

            var adminUser = new User()
            {
                UserName = "Admin",
                Password = PasswordHelper.Encrypt("Admin123"),
                Role = "Admin",
                CreatedDate = DateTime.UtcNow,
                CreateBy = "System",
                Status = 0
            };

            context.User.Add(adminUser);
            context.SaveChangesAsync();
        }
    }
}
