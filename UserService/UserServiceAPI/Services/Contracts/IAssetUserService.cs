using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAPI.DTO;

namespace UserServiceAPI.Services.Contracts
{
    public interface IAssetUserService
    {
        /// <summary>
        /// Create a new asset user assignment.
        /// </summary>
        /// <param name="assetUserDTO"></param>
        /// <returns></returns>
        Task<AssetUserDTO> Create(AssetUserDTO assetUserDTO);

        /// <summary>
        /// Delete a asset user assignment.
        /// </summary>
        /// <param name="assetUserDTO">The asset to be deleted.</param>
        void Delete(AssetUserDTO assetUserDTO);

        /// <summary>
        /// Update a asset user assignment.
        /// </summary>
        /// <param name="assetUserDTO">The asset user to be updated</param>
        void Update(AssetUserDTO assetUserDTO);

        /// <summary>
        /// Fetches and return a specific asset user assignment.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="assetId">The asset id.</param>
        /// <returns>The asset user assignment.</returns>
        Task<AssetUserDTO> FetchAsset(Guid userId, Guid assetId);

        /// <summary>
        /// Fetches and return all assets of a user.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <returns></returns>
        Task<IList<AssetUserDTO>> FetchAssetByUser(Guid userId);
    }
}
