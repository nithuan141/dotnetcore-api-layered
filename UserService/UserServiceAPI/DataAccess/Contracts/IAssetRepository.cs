using AssetServiceDataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAPI.DataAccess.Contracts;

namespace UserServiceAPI.DataAccess.Contracts
{
    public interface IAssetRepository : IRepositoryBase<Asset>
    {
    }
}
