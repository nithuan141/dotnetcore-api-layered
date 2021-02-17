using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiBase.Middlewares
{
    /// <summary>
    /// Extension of service collection for defining authorization middleware configurations.
    /// </summary>
    public static class AuthorizationConfigurations
    {
        /// <summary>
        /// The extension method to configure the authorization middleware with JWT.
        /// </summary>
        /// <param name="services">The service collection object.</param>
        /// <param name="configuration">The configuration object.</param>
        public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration.GetSection("JWT:secret").Value;
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
        }
    }
}
