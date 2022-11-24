using AutoMapper;
using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.ServicesDataAccess.DataAccess;

namespace DropStorage.WebApi.Services.Services
{
    public class LogStatusService
    {
        private readonly LogStatusDataAccess _logStatusDataAccess;
        private readonly IMapper _mapper;

        public LogStatusService(LogStatusDataAccess logStatusDataAccess, IMapper mapper)
        {
            _logStatusDataAccess = logStatusDataAccess;
            _mapper = mapper;
        }

        public async Task CreateLogStatus(LogStatus logStatus)
        {
            await _logStatusDataAccess.CreateLogStatus(logStatus);
        }
    }
}
