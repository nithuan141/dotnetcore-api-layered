using AssetServiceDataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAPI.DataAccess.Contracts;

namespace UserServiceAPI.DataAccess
{
    public class AssetRepository : RepositoryBase<Asset>, IAssetRepository
    {
        public AssetRepository(AssetServiceContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
