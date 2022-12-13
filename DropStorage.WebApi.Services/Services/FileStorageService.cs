using AutoMapper;
using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.ServicesDataAccess.DataAccess;
using DropStorage.WebApi.ServicesDataAccess.DTOs.FileStorage;

namespace DropStorage.WebApi.Services.Services
{
    public class FileStorageService
    {
        private readonly FileStorageDataAccess _fileStorageDataAccess;
        private readonly IMapper _mapper;

        public FileStorageService(FileStorageDataAccess fileStorageDataAccess, IMapper mapper)
        {
            _fileStorageDataAccess = fileStorageDataAccess;
            _mapper = mapper;
        }

        public async Task<List<FileStorage>> GetInfoFiles(List<Guid> ids)
        {
            return await _fileStorageDataAccess.GetFileStorageList(ids);
        }

        public async Task<List<FileStorageDTO>> GetAllFilesByUserId(Guid idUser)
        {
            List<FileStorage> fileStorageList = await _fileStorageDataAccess.GetAllFilesByUserId(idUser);
            List<FileStorageDTO> fileStorageDTOList = new List<FileStorageDTO>();
            return _mapper.Map(fileStorageList, fileStorageDTOList);
        }

        public async Task<bool> InsertDropStorageFile(FileStorage fileStorage)
        {
            return await _fileStorageDataAccess.InsertDropStorageFile(fileStorage);
        }

        public async Task<bool> DeleteFiles(List<Guid> ids)
        {
            return await _fileStorageDataAccess.DeleteFile(ids);
        }
    }
}
