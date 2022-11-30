using AutoMapper;
using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.ServicesDataAccess.DTOs.FileStorage;

namespace DropStorage.WebApi.ServicesDataAccess.Mappers
{
    public class FileStorageMapper : Profile
    {
        public FileStorageMapper()
        {
            CreateMap<FileStorage, FileStorageDTO>();
            CreateMap<FileStorageDTO, FileStorage>();
        }
    }
}
