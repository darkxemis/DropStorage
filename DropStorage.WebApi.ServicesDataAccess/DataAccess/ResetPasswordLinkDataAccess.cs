using DropStorage.WebApi.DataModel.Core;
using DropStorage.WebApi.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DropStorage.WebApi.ServicesDataAccess.DataAccess
{
    public class ResetPasswordLinkDataAccess : Service
    {

        private EFUnitOfWork<DropStorageContext> _EF;
        private readonly ILogger _logger;

        public ResetPasswordLinkDataAccess(EFUnitOfWork<DropStorageContext> ef, ILogger logger)
            : base(logger)
        {
            _EF = ef;
            _logger = logger;
        }

        public async Task<ResetPasswordLink> Create(ResetPasswordLink resetPasswordLink)
        {
            EFRepository<ResetPasswordLink> repo = this._EF.Repository<ResetPasswordLink>();
            repo.Insert(resetPasswordLink);

            await this._EF.SaveChangesAsync();

            return resetPasswordLink;
        }

        public async Task<ResetPasswordLink> GetById(Guid id)
        {
            EFRepository<ResetPasswordLink> repo = _EF.Repository<ResetPasswordLink>();
            ResetPasswordLink? resetPasswordLink = await repo.Query()
                .Where(request => request.Id == id)
                .FirstOrDefaultAsync();
            return resetPasswordLink;
        }
    }
}
