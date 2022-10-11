using AutoMapper;
using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.ServicesDataAccess.DTOs;
using DropStorage.WebApi.ServicesDataAccess.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropStorage.WebApi.ServicesDataAccess.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            //Create Modify
            CreateMap<CreateModifyUserDTO, User>();
        }
    }
}
