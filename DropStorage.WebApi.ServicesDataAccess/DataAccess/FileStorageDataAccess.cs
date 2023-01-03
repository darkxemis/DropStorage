using DropStorage.WebApi.DataModel.Core;
using DropStorage.WebApi.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropStorage.WebApi.ServicesDataAccess.DataAccess
{
    public class FileStorageDataAccess : Service
    {
        private EFUnitOfWork<DropStorageContext> _EF;
        private readonly ILogger _logger;

        public FileStorageDataAccess(EFUnitOfWork<DropStorageContext> ef, ILogger logger)
            : base(logger)
        {
            _EF = ef;
            _logger = logger;
        }

        public async Task<List<FileStorage>> GetFileStorageList(List<Guid> ids)
        {
            EFRepository<FileStorage> repo = _EF.Repository<FileStorage>();
            List<FileStorage> fileStorageList = await repo.Query().Where(file => ids.Contains(file.Id)).Include(file => file.ShareLinkFileStorages).ToListAsync();

            return fileStorageList;
        }

        public async Task<List<FileStorage>> GetAllFilesByUserId(Guid idUser)
        {
            EFRepository<FileStorage> repo = _EF.Repository<FileStorage>();
            List<FileStorage> fileStorageList = await repo.Query().Where(file => file.UserId == idUser).Include(file => file.User).OrderBy(file => file.Name).ToListAsync();

            return fileStorageList;
        }

        public async Task<bool> InsertDropStorageFile(FileStorage fileStorage)
        {
            bool isSaved = false;
            EFRepository<FileStorage> repo = _EF.Repository<FileStorage>();

            repo.Insert(fileStorage);
            isSaved = await this._EF.SaveChangesAsync();

            return isSaved;
        }

        public async Task<bool> DeleteFile(List<Guid> ids)
        {
            bool isRemoved = false;
            EFRepository<FileStorage> repo = _EF.Repository<FileStorage>();
            EFRepository<ShareLinkFileStorage> repoShare = _EF.Repository<ShareLinkFileStorage>();
            List<FileStorage> fileStorageDelte = await GetFileStorageList(ids);
            List<ShareLinkFileStorage> shareLinkFileStorage = repoShare.Query().Where(fileShared => fileStorageDelte
                .Select(file => file.Id)
                .Contains(fileShared.IdFileStorage))
                .ToList();

            repoShare.DeleteRange(shareLinkFileStorage);
            repo.DeleteRange(fileStorageDelte);
            isRemoved = await this._EF.SaveChangesAsync();

            return isRemoved;
        }
    }
}
