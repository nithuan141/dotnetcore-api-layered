using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAPI.DTO;

namespace UserServiceAPI.Services.Contracts
{
    public interface IUserService
    {
        /// <summary>
        /// Create a new user and returns the created user object.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        Task<UserDTO> Create(UserDTO userDTO);

        /// <summary>
        /// Update a user's details.
        /// </summary>
        /// <param name="userDTO">The user object.</param>
        void Update(UserDTO userDTO);

        /// <summary>
        /// Delete a given user.
        /// </summary>
        /// <param name="userDTO">The user object.</param>
        void Delete(UserDTO userDTO);

        /// <summary>
        /// Fetches and return all the users.
        /// </summary>
        /// <returns>List of users.</returns>
        Task<IList<UserDTO>> FetchAllUsers();

        /// <summary>
        /// Featches and returns a single user.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>A single user object.</returns>
        Task<UserDTO> GetUser(Guid userId);
    }
}
