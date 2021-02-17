using AutoMapper;
using AssetServiceDataProvider.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAPI.DataAccess.Contracts;
using UserServiceAPI.DTO;

namespace UserServiceAPI.Services.Contracts
{
    /// <summary>
    /// The asset user service class - for user assignmnet in assets.
    /// </summary>
    public class AssetUserService: IAssetUserService
    {
        /// <summary>
        /// Instnace of the asset user repository.
        /// </summary>
        private readonly IAssetUserRepository assetUserRepository;

        /// <summary>
        /// Automapper object.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// The asset user service constructor to inject instance of repository and mapper.
        /// </summary>
        /// <param name="_assetUserRepository">The asset user repository instnace.</param>
        /// <param name="_mapper">The auto mapper instance.</param>
        public AssetUserService(IAssetUserRepository _assetUserRepository, IMapper _mapper)
        {
            this.assetUserRepository = _assetUserRepository;
            this.mapper = _mapper;
        }

        /// <summary>
        /// Create a new asset user assignment.
        /// </summary>
        /// <param name="assetUserDTO"></param>
        /// <returns></returns>
        public async Task<AssetUserDTO> Create(AssetUserDTO assetUserDTO)
        {
            var asset = this.mapper.Map<AssetUser>(assetUserDTO);
            var createdAsset = await this.assetUserRepository.Create(asset);
            this.assetUserRepository.Save();
            assetUserDTO.AssetUserId = createdAsset.AssetUserId;
            return assetUserDTO;
        }

        /// <summary>
        /// Delete a asset user assignment.
        /// </summary>
        /// <param name="assetUserDTO">The asset to be deleted.</param>
        public void Delete(AssetUserDTO assetUserDTO)
        {
            var assetUser = this.mapper.Map<AssetUser>(assetUserDTO);
            this.assetUserRepository.Delete(assetUser);
            this.assetUserRepository.Save();
        }

        /// <summary>
        /// Update a asset user assignment.
        /// </summary>
        /// <param name="assetUserDTO">The asset user to be updated</param>
        public void Update(AssetUserDTO assetUserDTO)
        {
            var assetUser = this.mapper.Map<AssetUser>(assetUserDTO);
            this.assetUserRepository.Update(assetUser);
            this.assetUserRepository.Save();
        }

        /// <summary>
        /// Fetches and return a specific asset user assignment.
        /// </summary>
        /// <param name="userId">The user Guid.</param>
        /// <param name="assetId">The asset Guid.</param>
        /// <returns>The asset user assignment.</returns>
        public async Task<AssetUserDTO> FetchAsset(Guid userId, Guid assetId)
        {
            var asset = await this.assetUserRepository.FindByCondition(x => x.UserId.Equals(userId) && x.AssetId.Equals(assetId)).FirstOrDefaultAsync();
            return this.mapper.Map<AssetUserDTO>(asset);
        }

        /// <summary>
        /// Fetches and return all assets of a user.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <returns>List of assets assigned to the user.</returns>
        public async Task<IList<AssetUserDTO>> FetchAssetByUser(Guid userId)
        {
            var asset = await this.assetUserRepository.FindByCondition(x => x.UserId.Equals(userId)).ToListAsync();
            return this.mapper.Map<IList<AssetUserDTO>>(asset);
        }
    }
}
