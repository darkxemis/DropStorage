using AutoMapper;
using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.ServicesDataAccess.DTOs;

namespace DropStorage.WebApi.ServicesDataAccess.Mappers
{
    public class RolMapper : Profile
    {
        public RolMapper()
        {
            CreateMap<Rol, RolDTO>();
            CreateMap<RolDTO, Rol>();
        }
    }
}
