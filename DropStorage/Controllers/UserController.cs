using DropStorage.WebApi.Services.Services;
using DropStorage.WebApi.ServicesDataAccess.DTOs.User;
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


        [Route("api/user/create")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> Token(CreateModifyUserDTO createModifyUserDTO)
        {
            return await this._userService.CreateUser(createModifyUserDTO);
        }
    }
}
