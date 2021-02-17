using AutoMapper;
using AssetServiceDataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAPI.DTO;

namespace UserServiceAPI.Middlewares
{
    /// <summary>
    /// Automapper mapper class.
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Mapper profile constructor.
        /// </summary>
        public MapperProfile()
        {
            this.CreateMap<User, UserDTO>().ReverseMap();
            this.CreateMap<Asset, AssetDTO>().ReverseMap();
            this.CreateMap<AssetUser, AssetUserDTO>().ReverseMap();
        }
    }
}
