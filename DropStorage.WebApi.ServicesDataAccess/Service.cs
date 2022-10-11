using Microsoft.Extensions.Logging;

namespace DropStorage.WebApi.ServicesDataAccess
{
    public abstract class Service
    {
        protected ILogger Logger { get; private set; }

        public Service(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentException($"Service: {typeof(Service).Name} is null", "logger");
            }

            this.Logger = logger;
        }
    }
}
