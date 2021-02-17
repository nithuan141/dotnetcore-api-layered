using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserServiceAPI.DTO;
using UserServiceAPI.Services.Contracts;

namespace UserServiceAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/user")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationService authenticationService;

        /// <summary>
        /// The authentication controller constructor which calls upon instantiating the controller.
        /// </summary>
        /// <param name="_authenticationService">Injected instance of UserAuthenticationService.</param>
        public AuthenticationController(IUserAuthenticationService _authenticationService)
        {
            this.authenticationService = _authenticationService;
        }

        /// <summary>
        /// Authentication endpoint.
        /// </summary>
        /// <param name="user">The user details - user name and password. </param>
        /// <returns></returns>
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            var loggedInUser = await this.authenticationService.Authenticate(user);
            if(loggedInUser == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(loggedInUser);
            }
        }
    }
}
