using AssetServiceDataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserServiceAPI.DataAccess.Contracts
{
    public interface IUserRepository: IRepositoryBase<User>
    {
    }
}
