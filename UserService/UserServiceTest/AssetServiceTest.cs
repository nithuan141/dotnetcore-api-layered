using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UserServiceAPI.Services;
using UserServiceAPI.DTO;
using UserServiceAPI.DataAccess.Contracts;
using AutoMapper;
using System.Threading.Tasks;
using AssetServiceDataProvider.Models;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Linq;
using MockQueryable.Moq;
using UserServiceAPI.Middlewares;
using UserServiceAPI.Services.Contracts;

namespace UserServiceTest
{
    [TestClass]
    public class AssetServiceTest
    {
        private readonly IMapper mapperMock;
        private readonly IAssetService assetService;

        /// <summary>
        /// Construtor, configuring the mock objects and automapper
        /// </summary>
        public AssetServiceTest()
        {
            // Mapper configuration
            var myProfile = new MapperProfile();
            var _config = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapperMock = new Mapper(_config);

            // Repository mock object and method setup.
            var assetRepoMock = new Mock<IAssetRepository>();
            var mockData = AssetMockdata.AsQueryable().BuildMock();
            assetRepoMock.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Asset, bool>>>())).Returns(mockData.Object);
            assetRepoMock.Setup(x => x.FindAll()).Returns(mockData.Object);
            assetRepoMock.Setup(x => x.Create(It.IsAny<Asset>())).Returns(Task.FromResult(this.AssetMockdata.First()));
            assetRepoMock.Setup(x => x.Update(It.IsAny<Asset>()));
            assetRepoMock.Setup(x => x.Delete(It.IsAny<Asset>()));

            //The service instance.
            assetService = new AssetService(assetRepoMock.Object, mapperMock);
        }

        [TestMethod]
        public async Task Call_Method_Create_Returns_Object()
        {
            var createdasset = await assetService.Create(AssetDTOMockdata.First());

            Assert.IsNotNull(createdasset);
            Assert.IsNotNull(createdasset.AssetId);
        }

        [TestMethod]
        public void Call_Method_Update_Returns_Nothing()
        {
            // Making sure whether the update is called without any exceptions.
            try
            {
                this.assetService.Update(this.AssetDTOMockdata.First());
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Call_Method_Delete_Returns_Nothing()
        {
            // Making sure whether the delete is called without any erros.
            try
            {
                this.assetService.Delete(this.AssetDTOMockdata.First());
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task Call_Method_FetchAllAssets_Returns_List()
        {
            var assets = await assetService.FetchAllAssets();

            Assert.IsNotNull(assets);
            Assert.AreEqual(1, assets.Count());
        }

        [TestMethod]
        public async Task Call_MethodFetchAsset_Returns_Object()
        {
            var asset = await assetService.FetchAsset(Guid.NewGuid());

            Assert.IsNotNull(asset);
            Assert.IsNotNull(asset.AssetId);
        }

        /// <summary>
        /// The test asset data.
        /// </summary>
        private List<Asset> AssetMockdata
        {
            get
            {
                return new List<Asset>()
                {
                  new Asset() 
                  {
                        AssetId = Guid.NewGuid(),
                        AssetName = "Main Asset",
                        Description = "Main",
                        CreateBy = "Admin",
                        CreatedDate = DateTime.UtcNow
                  }
                };
            }
        }

        /// <summary>
        /// The test asset DTO data.
        /// </summary>
        private List<AssetDTO> AssetDTOMockdata
        {
            get
            {
                return new List<AssetDTO>()
                {
                  new AssetDTO()
                  {
                        AssetId = Guid.NewGuid(),
                        AssetName = "Main Asset",
                        Description = "Main",
                        CreateBy = "Admin",
                        CreatedDate = DateTime.UtcNow
                  }
                };
            }
        }
    }
}
