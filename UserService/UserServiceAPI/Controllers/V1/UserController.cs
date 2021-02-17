using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBase.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserServiceAPI.DTO;
using UserServiceAPI.Services.Contracts;

namespace UserServiceAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Object of user service.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// User controller constructor, injecting the service instances.
        /// </summary>
        /// <param name="_userService">Instance of the user service.</param>
        public UserController(IUserService _userService)
        {
            this.userService = _userService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(UserDTO user)
        {
            user.CreateBy = await this.HttpContext.LoggedInUser();
            user.CreatedDate = DateTime.UtcNow;
            var createdUser = await this.userService.Create(user);
            return Ok(createdUser.UserId);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(UserDTO user)
        {
            this.userService.Update(user);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(UserDTO user)
        {
            this.userService.Delete(user);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var users = await this.userService.FetchAllUsers();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var user = await this.userService.GetUser(userId);
            return Ok(user);
        }
    }
}



