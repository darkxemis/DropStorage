using AutoMapper;
using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.ServicesDataAccess.DTOs;

namespace DropStorage.WebApi.ServicesDataAccess.Mappers
{
    internal class RolMapper : Profile
    {
        public RolMapper()
        {
            CreateMap<Rol, RolDTO>();
            CreateMap<RolDTO, Rol>();
        }
    }
}
