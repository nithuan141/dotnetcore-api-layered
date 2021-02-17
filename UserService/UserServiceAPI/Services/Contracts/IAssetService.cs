using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAPI.DTO;

namespace UserServiceAPI.Services.Contracts
{
    public interface IAssetService
    {
        /// <summary>
        /// Create a new asset
        /// </summary>
        /// <param name="assetDTO"></param>
        /// <returns></returns>
        Task<AssetDTO> Create(AssetDTO assetDTO);

        /// <summary>
        /// Delete a asset.
        /// </summary>
        /// <param name="assetDTO">The asset to be deleted.</param>
        void Delete(AssetDTO assetDTO);

        /// <summary>
        /// Update a  asset.
        /// </summary>
        /// <param name="assetDTO">The asset to be updated</param>
        void Update(AssetDTO assetDTO);

        /// <summary>
        /// Featches and returns all the asset.
        /// </summary>
        /// <returns></returns>
        Task<IList<AssetDTO>> FetchAllAssets();

        /// <summary>
        /// Featches and return a specific asset.
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        Task<AssetDTO> FetchAsset(Guid assetId);
    }
}
