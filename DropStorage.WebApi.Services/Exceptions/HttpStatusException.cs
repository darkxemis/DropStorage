using DropStorage.WebApi.Services.Constants;
using System.Net;

namespace DropStorage.WebApi.Services.Exceptions
{
    public class HttpStatusException : Exception
    {
        public HttpStatusCode Status { get; private set; }

        public int StatusCode { get { return (int)Status; } }

        public HttpStatusException(HttpStatusCode status, string msg) : base(msg)
        {
            this.Status = status;
        }
    }
}
