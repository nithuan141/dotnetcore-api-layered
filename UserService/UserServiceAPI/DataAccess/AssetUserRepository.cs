using AssetServiceDataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAPI.DataAccess.Contracts;

namespace UserServiceAPI.DataAccess
{
    public class AssetUserRepository : RepositoryBase<AssetUser>, IAssetUserRepository
    {
        /// <summary>
        /// Constructor of repository class.
        /// </summary>
        /// <param name="repositoryContext"></param>
        public AssetUserRepository(AssetServiceContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
