using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.Services.Exceptions;
using DropStorage.WebApi.Services.Services;
using DropStorage.WebApi.ServicesDataAccess.DTOs;
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
        private readonly LogStatusService _logStatusService;
        private readonly UserService _userService;

        public ErrorController(ILogger logger, LogStatusService logStatusService, UserService userService)
        {
            _logger = logger;
            _logStatusService = logStatusService;
            _userService = userService;
        }

        [Route("error-local-development")]
        public async Task<ErrorResponse> ErrorLocalDevelopment()
        {
            UserDTO? user = null;
            string userName = User.Identity.Name;

            if (userName != null)
            {
                user = await _userService.GetUserByName(userName);
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error; // Your exception
            var code = 500; // Internal Server Error by default

            _logger.LogError(exception.Message);
            await _logStatusService.CreateLogStatus(new LogStatus()
            {
                CreateTime = DateTime.Now,
                Description = exception.Message,
                Endpoint = "error-local-development",
                IsError = true,
                UserId = user?.Id
            });

            //if (exception is LogInException) code = 401; // Unauthorized
            //else if (exception is LogInException) code = 404; // Not Found
            //else if (exception is MyException) code = 400; // Bad Request

            Response.StatusCode = code;

            return new ErrorResponse(exception); // Error model
        }

        [Route("/error")]
        public IActionResult HandleError() => Problem();
    }
}
