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
    public class AssetUserServiceTest
    {
        private readonly IMapper mapperMock;
        private readonly IAssetUserService assetUserService;

        /// <summary>
        /// Construtor, configuring the mock objects and automapper
        /// </summary>
        public AssetUserServiceTest()
        {
            // Mapper configuration
            var myProfile = new MapperProfile();
            var _config = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapperMock = new Mapper(_config);

            // Repository mock object and method setup.
            var assetUserRepoMock = new Mock<IAssetUserRepository>();
            var mockData = AssetUserMockdata.AsQueryable().BuildMock();
            assetUserRepoMock.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<AssetUser, bool>>>())).Returns(mockData.Object);
            assetUserRepoMock.Setup(x => x.FindAll()).Returns(mockData.Object);
            assetUserRepoMock.Setup(x => x.Create(It.IsAny<AssetUser>())).Returns(Task.FromResult(this.AssetUserMockdata.First()));
            assetUserRepoMock.Setup(x => x.Update(It.IsAny<AssetUser>()));
            assetUserRepoMock.Setup(x => x.Delete(It.IsAny<AssetUser>()));

            //The service instance.
            assetUserService = new AssetUserService(assetUserRepoMock.Object, mapperMock);
        }

        [TestMethod]
        public async Task Call_Method_Create_Returns_Object()
        {
            var createdassetUser = await assetUserService.Create(AssetUserDTOMockdata.First());

            Assert.IsNotNull(createdassetUser);
            Assert.IsNotNull(createdassetUser.AssetUserId);
        }

        [TestMethod]
        public void Call_Method_Update_Returns_Nothing()
        {
            // Making sure whether the update is called without any exceptions.
            try
            {
                this.assetUserService.Update(this.AssetUserDTOMockdata.First());
            }
            catch (Exception)
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
                this.assetUserService.Delete(this.AssetUserDTOMockdata.First());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task Call_Method_FetchAsset_Returns_List()
        {
            var assetUsers = await assetUserService.FetchAsset(Guid.NewGuid(), Guid.NewGuid());

            Assert.IsNotNull(assetUsers);
        }

        [TestMethod]
        public async Task Call_MethodFetchAssetUser_Returns_Object()
        {
            var assetUser = await assetUserService.FetchAssetByUser(Guid.NewGuid());

            Assert.IsNotNull(assetUser);
            Assert.AreEqual(1, assetUser.Count());
        }

        /// <summary>
        /// The test assetUser data.
        /// </summary>
        private List<AssetUser> AssetUserMockdata
        {
            get
            {
                return new List<AssetUser>()
                {
                  new AssetUser() 
                  {
                        AssetUserId =1,
                        AssetId = Guid.NewGuid(),
                        UserId = Guid.NewGuid(),
                        CreateBy = "Admin",
                        CreatedDate = DateTime.UtcNow
                  }
                };
            }
        }

        /// <summary>
        /// The test asset user DTO data.
        /// </summary>
        private List<AssetUserDTO> AssetUserDTOMockdata
        {
            get
            {
                return new List<AssetUserDTO>()
                {
                  new AssetUserDTO()
                  {
                        AssetUserId =1,
                        AssetId = Guid.NewGuid(),
                        UserId = Guid.NewGuid(),
                        CreateBy = "Admin",
                        CreatedDate = DateTime.UtcNow
                  }
                };
            }
        }
    }
}
