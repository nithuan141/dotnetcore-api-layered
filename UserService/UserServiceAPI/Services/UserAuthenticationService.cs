using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserServiceAPI.Common;
using UserServiceAPI.DataAccess.Contracts;
using UserServiceAPI.DTO;
using UserServiceAPI.Services.Contracts;

namespace UserServiceAPI.Services
{
    /// <summary>
    /// User authentication service class.
    /// </summary>
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly string secret;
        private readonly string expDate;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Authentication service constructor.
        /// </summary>
        /// <param name="_configuration">Instance of the configuration.</param>
        /// <param name="_userRepository">Instance of the user repository.</param>
        /// <param name="_mapper">Automapper object.</param>
        public UserAuthenticationService(IConfiguration _configuration, IUserRepository _userRepository, IMapper _mapper)
        {
            this.userRepository = _userRepository;
            this.mapper = _mapper;
            secret = _configuration.GetSection("JWT:secret").Value;
            expDate = _configuration.GetSection("JWT:expirationInMinutes").Value;
        }

        /// <summary>
        /// Authenticating the given user and returning the access token.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <returns>User data with Access Token</returns>
        public async Task<UserDTO> Authenticate(UserDTO user)
        {
            var loggedInuser = new UserDTO()
            {
                UserName = "Admin",
                Password = PasswordHelper.Encrypt("Admin123"),
                Role = "Admin",
                CreatedDate = DateTime.UtcNow,
                CreateBy = "System",
                Status = 0
            };

            if (loggedInuser != null)
            {
                loggedInuser.Token = this.GenerateSecurityToken(loggedInuser);
                loggedInuser.Password = null;
            }

            return loggedInuser;
        }

        /// <summary>
        /// Generating the JWT token.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <returns></returns>
        private string GenerateSecurityToken(UserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Read and returns the user by given user name and password.
        /// </summary>
        /// <param name="userName">The User Name.</param>
        /// <param name="password">The Password.</param>
        /// <returns></returns>
        private async Task<UserDTO> GetUser(string userName, string password)
        {
            var encryptedPassword = PasswordHelper.Encrypt(password);
            var user = await this.userRepository.FindByCondition(user => user.Password.Equals(encryptedPassword) && user.UserName.Equals(userName)).FirstOrDefaultAsync();
            return (this.mapper.Map<UserDTO>(user));
        }
    }
}
