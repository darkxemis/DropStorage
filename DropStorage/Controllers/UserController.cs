using DropStorage.WebApi.Services.Services;
using DropStorage.WebApi.ServicesDataAccess.DTOs.Auth;
using DropStorage.WebApi.ServicesDataAccess.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DropStorage.Controllers
{
    [ApiController]
    public class UserController
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [Route("api/auth/token")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<AuthDTO> Token([FromBody] AccessDTO access)
        {
            return await this._userService.Token(access);
        }

        [Route("api/user/create")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> Token([FromBody] CreateModifyUserDTO createModifyUserDTO)
        {
            return await this._userService.CreateUser(createModifyUserDTO);
        }

        [Authorize]
        [Route("api/user/prueba")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<string> agg()
        {
            return "";
        }
    }
}
