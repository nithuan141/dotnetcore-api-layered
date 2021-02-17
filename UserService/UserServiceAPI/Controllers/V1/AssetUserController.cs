using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBase.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserServiceAPI.DTO;
using UserServiceAPI.Services.Contracts;

namespace UserServiceAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AssetUserController : ControllerBase
    {
        /// <summary>
        /// Object of asset user service.
        /// </summary>
        private readonly IAssetUserService assetUserService;

        /// <summary>
        /// User controller constructor, injecting the service instances.
        /// </summary>
        /// <param name="_assetUserService">Instance of the asset user service.</param>
        public AssetUserController(IAssetUserService _assetUserService)
        {
            this.assetUserService = _assetUserService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(AssetUserDTO assetUser)
        {
            assetUser.CreateBy = await this.HttpContext.LoggedInUser();
            assetUser.CreatedDate = DateTime.UtcNow;
            var createdAsset = await this.assetUserService.Create(assetUser);
            return Ok(createdAsset.AssetId);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(AssetUserDTO assetUser)
        {
            this.assetUserService.Update(assetUser);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(AssetUserDTO assetUser)
        {
            this.assetUserService.Delete(assetUser);
            return Ok();
        }

        [HttpGet]
        [Route("User/{userId}/Asset")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var assets = await this.assetUserService.FetchAssetByUser(userId);
            return Ok(assets);
        }

        [HttpGet("User/{userId}/asset/{assetId}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid userId, Guid assetId)
        {
            await this.HttpContext.ValidateSelfAccess(userId);
            var assets = await this.assetUserService.FetchAsset(userId, assetId);
            return Ok(assets);
        }
    }
}
