using System;
using Microsoft.EntityFrameworkCore;

namespace AssetServiceDataProvider.Models
{
    public partial class AssetServiceContext : DbContext
    {
        public AssetServiceContext()
        {
        }

        public AssetServiceContext(DbContextOptions<AssetServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Asset> Asset { get; set; }
        public virtual DbSet<AssetUser> AssetUser { get; set; }
    }
}
