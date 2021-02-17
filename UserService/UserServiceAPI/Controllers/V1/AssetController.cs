using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBase.Common;
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
    public class AssetController : ControllerBase
    {
        /// <summary>
        /// Object of asset service.
        /// </summary>
        private readonly IAssetService assetService;

        /// <summary>
        /// User controller constructor, injecting the service instances.
        /// </summary>
        /// <param name="_assetService">Instance of the asset service.</param>
        public AssetController(IAssetService _assetService)
        {
            this.assetService = _assetService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(AssetDTO asset)
        {
            asset.CreateBy = await this.HttpContext.LoggedInUser();
            asset.CreatedDate = DateTime.UtcNow;
            var createdAsset = await this.assetService.Create(asset);
            return Ok(createdAsset.AssetId);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(AssetDTO asset)
        {
            this.assetService.Update(asset);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(AssetDTO asset)
        {
            this.assetService.Delete(asset);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var assets = await this.assetService.FetchAllAssets();
            return Ok(assets);
        }

        [HttpGet("{assetId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(Guid assetId)
        {
            var user = await this.assetService.FetchAsset(assetId);
            return Ok(user);
        }
    }
}
