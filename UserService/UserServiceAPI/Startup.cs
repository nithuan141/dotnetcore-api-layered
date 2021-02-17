using ApiBase.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UserServiceAPI.Middlewares;

namespace UserServiceAPI
{
    /// <summary>
    /// The startup class, instantiating on starting the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The name of the Cors policy.
        /// </summary>
        private const string CORS_POLICY_NAME = "_AllowSpecificOrigins";

        /// <summary>
        /// Name of the API
        /// </summary>
        private const string API_NAME = "User Service API";

        /// <summary>
        /// The configuration instance.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Thre startup class constructor.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Using  this method to add services to the container.
        /// </summary>
        /// <param name="services">Service collection object.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDB(this.Configuration);

            services.ConfigureAPIVersioning();

            services.AddControllers();

            services.ConfigureValidators();

            services.ConfigureSwagger(API_NAME, "API for user management in Asset Service.");

            services.ConfigureAuth(this.Configuration);

            services.ConfigureDI();

            services.ConfigureLogger(this.Configuration);

            services.ConfigureCors(CORS_POLICY_NAME, this.Configuration);

            services.AddMapperConfiguration(new MapperProfile());
        }

        /// <summary>
        /// This method gets called by the runtime, configuring the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="logger">The logger object.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // // app.SetUpInitialData();

            app.UseSwaggerDocumentation(API_NAME);

            app.UseGlobalExceptionHandler(logger);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(CORS_POLICY_NAME);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
