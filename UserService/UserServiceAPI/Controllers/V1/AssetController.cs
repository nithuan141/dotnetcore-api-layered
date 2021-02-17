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
    /// <summary>
    /// The asset controller class.
    /// </summary>
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

        /// <summary>
        /// Asset creation.
        /// </summary>
        /// <param name="asset">Asset model object.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(AssetDTO asset)
        {
            asset.CreateBy = await this.HttpContext.LoggedInUser();
            asset.CreatedDate = DateTime.UtcNow;
            var createdAsset = await this.assetService.Create(asset);
            return Ok(createdAsset.AssetId);
        }

        /// <summary>
        /// Asset updation.
        /// </summary>
        /// <param name="asset">Asset model object.</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(AssetDTO asset)
        {
            this.assetService.Update(asset);
            return Ok();
        }

        /// <summary>
        /// Asset deletion.
        /// </summary>
        /// <param name="asset">Asset model object.</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(AssetDTO asset)
        {
            this.assetService.Delete(asset);
            return Ok();
        }

        /// <summary>
        /// API end point to fetch and return all assets.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var assets = await this.assetService.FetchAllAssets();
            return Ok(assets);
        }

        /// <summary>
        /// Fethces a specific asset and return that.
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        [HttpGet("{assetId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(Guid assetId)
        {
            var user = await this.assetService.FetchAsset(assetId);
            return Ok(user);
        }
    }
}
