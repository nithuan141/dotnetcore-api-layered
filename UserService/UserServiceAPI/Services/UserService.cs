using AutoMapper;
using AssetServiceDataProvider.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAPI.Common;
using UserServiceAPI.DataAccess.Contracts;
using UserServiceAPI.DTO;
using UserServiceAPI.Services.Contracts;

namespace UserServiceAPI.Services
{
    /// <summary>
    /// The user service implemtation.
    /// </summary>
    public class UserService: IUserService
    {
        /// <summary>
        /// Instnace of the user repository.
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Automapper object.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// The user service constructor.
        /// </summary>
        /// <param name="_userRepository">Instance of the user repository.</param>
        /// <param name="_mapper">Instance of teh auto mapper.</param>
        public UserService(IUserRepository _userRepository, IMapper _mapper)
        {
            this.userRepository = _userRepository;
            this.mapper = _mapper;
        }

        /// <summary>
        /// Create a new user and returns the created user object.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            var userEntity = this.mapper.Map<User>(userDTO);
            userEntity.Password = PasswordHelper.Encrypt(userDTO.Password);
            var createdUser = await this.userRepository.Create(userEntity);
            this.userRepository.Save();
            return this.mapper.Map<UserDTO>(createdUser);
        }

        /// <summary>
        /// Update a user's details.
        /// </summary>
        /// <param name="userDTO">The user object.</param>
        public void Update(UserDTO userDTO)
        {
            var userEntity = this.mapper.Map<User>(userDTO);
            userEntity.UpdatedDate = DateTime.UtcNow;
            this.userRepository.Update(userEntity);
            this.userRepository.Save();
        }

        /// <summary>
        /// Delete a given user.
        /// </summary>
        /// <param name="userDTO">The user object.</param>
        public void Delete(UserDTO userDTO)
        {
            var userEntity = this.mapper.Map<User>(userDTO);
            this.userRepository.Delete(userEntity);
            this.userRepository.Save();
        }

        /// <summary>
        /// Fetches and return all the users.
        /// </summary>
        /// <returns>List of users.</returns>
        public async Task<IList<UserDTO>> FetchAllUsers()
        {
            var users = await this.userRepository.FindAll().ToListAsync();
            return this.mapper.Map<IList<UserDTO>>(users);
        }

        /// <summary>
        /// Featches and returns a single user.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>A single user object.</returns>
        public async Task<UserDTO> GetUser(Guid userId)
        {
            var users = await this.userRepository.FindByCondition(x => x.UserId == userId).FirstOrDefaultAsync();
            return this.mapper.Map<UserDTO>(users);
        }
    }
}
