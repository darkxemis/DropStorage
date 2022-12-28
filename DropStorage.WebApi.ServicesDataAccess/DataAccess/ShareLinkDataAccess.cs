using DropStorage.WebApi.DataModel.Core;
using DropStorage.WebApi.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DropStorage.WebApi.ServicesDataAccess.DataAccess
{
    public class ShareLinkDataAccess : Service
    {
        private EFUnitOfWork<DropStorageContext> _EF;
        private readonly ILogger _logger;

        public ShareLinkDataAccess(EFUnitOfWork<DropStorageContext> ef, ILogger logger)
          : base(logger)
        {
            _EF = ef;
            _logger = logger;
        }

        public async Task<ShareLink> GetById(Guid id)
        {
            EFRepository<ShareLink> repo = _EF.Repository<ShareLink>();
            ShareLink? shareLink = await repo.Query()
                .Where(request => request.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return shareLink;
        }

        public async Task<ShareLink> CreateShareLink(ShareLink shareLink, List<Guid> idsFileStorage)
        {
            List<ShareLinkFileStorage> shareLinkFileStorageList = new List<ShareLinkFileStorage>();
            bool isSaved = false;
            try
            {
                await _EF.BeginTransactionAsync();

                //ShareLink
                EFRepository<ShareLink> repo = _EF.Repository<ShareLink>();
                repo.Insert(shareLink);
                await _EF.SaveChangesAsync();

                //ShareLinkFileStorage
                EFRepository<ShareLinkFileStorage> shareLinkFileStorageRepo = _EF.Repository<ShareLinkFileStorage>();
                idsFileStorage.ForEach(idFileStorage =>
                {
                    shareLinkFileStorageList.Add(new ShareLinkFileStorage()
                    {
                        IdShareLink = shareLink.Id,
                        IdFileStorage = idFileStorage
                    });
                });
                shareLinkFileStorageRepo.InsertRange(shareLinkFileStorageList);
                isSaved = await _EF.SaveChangesAsync();

                await _EF.CommitTransactionAsync();
                
                return shareLink;
            } catch (Exception ex)
            {
                await _EF.RollbackTransactionAsync();
            }

            return null;
        }

        public async Task<List<ShareLinkFileStorage>> GetShareLinkFileStorageById(Guid idShareLink)
        {
            EFRepository<ShareLinkFileStorage> repo = _EF.Repository<ShareLinkFileStorage>();
            List<ShareLinkFileStorage>? shareLinkFileStorage = await repo.Query()
                .Where(request => request.IdShareLink == idShareLink)
                .Include(request => request.IdFileStorageNavigation)
                .Include(request => request.IdShareLinkNavigation)
                .ToListAsync();

            return shareLinkFileStorage;
        }
    }
}
