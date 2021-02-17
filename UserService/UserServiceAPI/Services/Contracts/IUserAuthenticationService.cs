using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAPI.DTO;

namespace UserServiceAPI.Services.Contracts
{
    /// <summary>
    /// The authentication service interface.
    /// </summary>
    public interface IUserAuthenticationService
    {
        /// <summary>
        /// Authenticating the given user and returning the access token.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <returns>User data with Access Token</returns>
        Task<UserDTO> Authenticate(UserDTO user);
    }
}
