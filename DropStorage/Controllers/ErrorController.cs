using DropStorage.WebApi.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DropStorage.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger _logger;

        public ErrorController(ILogger logger)
        {
            _logger = logger;
        }

        [Route("error-local-development")]
        public ErrorResponse ErrorLocalDevelopment()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error; // Your exception
            var code = 500; // Internal Server Error by default

            _logger.LogError(exception.Message);
            //if (exception is LogInException) code = 401; // Unauthorized
            //else if (exception is LogInException) code = 404; // Not Found
            //else if (exception is MyException) code = 400; // Bad Request

            Response.StatusCode = code;

            return new ErrorResponse(exception); // Error model
        }

        [Route("error")]
        public IActionResult Error() => Problem();
    }
}
