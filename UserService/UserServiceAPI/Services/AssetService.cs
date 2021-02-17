using AutoMapper;
using AssetServiceDataProvider.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAPI.DataAccess.Contracts;
using UserServiceAPI.DTO;
using UserServiceAPI.Services.Contracts;

namespace UserServiceAPI.Services
{
    /// <summary>
    /// The asset service class - to manage assets.
    /// </summary>
    public class AssetService: IAssetService
    {
        /// <summary>
        /// Instnace of the Asset repository.
        /// </summary>
        private readonly IAssetRepository assetRepository;

        /// <summary>
        /// Automapper object.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// The Asset service constructor to inject instance of repository and mapper.
        /// </summary>
        /// <param name="_assetRepository">The asset repository instnace.</param>
        /// <param name="_mapper">The auto mapper instance.</param>
        public AssetService(IAssetRepository _assetRepository, IMapper _mapper)
        {
            this.assetRepository = _assetRepository;
            this.mapper = _mapper;
        }

        /// <summary>
        /// Create a new Asset
        /// </summary>
        /// <param name="assetDTO"></param>
        /// <returns></returns>
        public async Task<AssetDTO> Create(AssetDTO assetDTO)
        {
            var asset = this.mapper.Map<Asset>(assetDTO);
            var createdAsset = await this.assetRepository.Create(asset);
            this.assetRepository.Save();
            assetDTO.AssetId = createdAsset.AssetId;
            return assetDTO;
        }

        /// <summary>
        /// Delete a Asset
        /// </summary>
        /// <param name="assetDTO">The asset to be deleted.</param>
        public void Delete(AssetDTO assetDTO)
        {
            var asset = this.mapper.Map<Asset>(assetDTO);
            this.assetRepository.Delete(asset);
            this.assetRepository.Save();
        }

        /// <summary>
        /// Update a Asset
        /// </summary>
        /// <param name="assetDTO">The asset to be updated</param>
        public void Update(AssetDTO assetDTO)
        {
            var asset = this.mapper.Map<Asset>(assetDTO);
            this.assetRepository.Update(asset);
            this.assetRepository.Save();
        }

        /// <summary>
        /// Fetches and returns all the Asset.
        /// </summary>
        /// <returns>The list of assets.</returns>
        public async Task<IList<AssetDTO>> FetchAllAssets()
        {
            var assetList = await this.assetRepository.FindAll().ToListAsync();
            return this.mapper.Map<IList<AssetDTO>>(assetList);
        }

        /// <summary>
        /// Fetches and return a specific Asset.
        /// </summary>
        /// <param name="assetId">The Asset guid.</param>
        /// <returns>The Asset.</returns>
        public async Task<AssetDTO> FetchAsset(Guid assetId)
        {
            var asset = await this.assetRepository.FindByCondition(x=>x.AssetId == assetId).FirstOrDefaultAsync();
            return this.mapper.Map<AssetDTO>(asset);
        }
    }
}
