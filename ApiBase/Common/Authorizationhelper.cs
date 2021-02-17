using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiBase.Common
{
    /// <summary>
    /// The HttpContext class extension helper class for authorization.
    /// </summary>
    public static class Authorizationhelper
    {
        /// <summary>
        /// Httpcontext extension method to validate whether the authorized user is accessing the resouces of self or not.
        /// <param name="httpContext">The http context</param>
        /// <param name="userId">The user id of the resource owner.</param>
        /// <returns></returns>
        public static async Task ValidateSelfAccess(this HttpContext httpContext, Guid userId)
        {
            var user = await GetUser(httpContext);
            var loggedinUserId = user.Claims.First(claim => claim.Type == "nameid")?.Value;

            if (loggedinUserId != userId.ToString())
            {
                throw new UnauthorizedAccessException("Not autherized to access this resource.");
            }
        }

        /// <summary>
        /// Httpcontext extension method to validate whether the authorized user is accessing the resouces of self or not.
        /// <param name="httpContext">The http context</param>
        /// <param name="userId">The user id of the resource owner.</param>
        /// <returns></returns>
        public static async Task ValidateSelfOrAdminAccess(this HttpContext httpContext, Guid? userId)
        {
            var user = await GetUser(httpContext);
            var loggedinUserId = user.Claims.First(claim => claim.Type == "nameid")?.Value;
            var role = user.Claims.First(claim => claim.Type == "role")?.Value;

            if (role != "Admin" && !userId.HasValue && loggedinUserId != userId.ToString())
            {
                throw new UnauthorizedAccessException("Not autherized to access this resource.");
            }
        }

        /// <summary>
        /// Fid and returns the logged in user name from the token.
        /// </summary>
        /// <param name="httpContext">The httpcontext</param>
        /// <returns></returns>
        public static async Task<string> LoggedInUser(this HttpContext httpContext)
        {
            var user = await GetUser(httpContext);
            var loggedinUsername = user.Claims.First(claim => claim.Type == "unique_name")?.Value;

            return loggedinUsername;
        }

        /// <summary>
        /// returns the user identity with claims as json.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static async Task<JwtSecurityToken> GetUser(HttpContext httpContext)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = await httpContext.GetTokenAsync("access_token");
            var user = handler.ReadToken(token) as JwtSecurityToken;
            return user;
        }
    }
}
