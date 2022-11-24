using DropStorage.WebApi.DataModel.Core;
using DropStorage.WebApi.DataModel.Models;
using Microsoft.Extensions.Logging;

namespace DropStorage.WebApi.ServicesDataAccess.DataAccess
{
    public class LogStatusDataAccess : Service
    {
        private EFUnitOfWork<DropStorageContext> _EF;
        private readonly ILogger _logger;

        public LogStatusDataAccess(EFUnitOfWork<DropStorageContext> ef, ILogger logger)
            : base(logger)
        {
            _EF = ef;
            _logger = logger;
        }

        public async Task<bool> CreateLogStatus(LogStatus logStatus)
        {
            bool isSaved = false;

            EFRepository<LogStatus> repo = this._EF.Repository<LogStatus>();
            repo.Insert(logStatus);

            isSaved = await this._EF.SaveChangesAsync();

            return isSaved;
        }

    }
}
