using AssetServiceDataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAPI.DataAccess.Contracts;

namespace UserServiceAPI.DataAccess
{
    /// <summary>
    /// The repository class for user.
    /// </summary>
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        /// <summary>
        /// Constructor of repository class.
        /// </summary>
        /// <param name="repositoryContext"></param>
        public UserRepository(AssetServiceContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
