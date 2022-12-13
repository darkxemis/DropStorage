using DropStorage.WebApi.Services.Services;
using DropStorage.WebApi.ServicesDataAccess.DTOs;
using DropStorage.WebApi.ServicesDataAccess.DTOs.Auth;
using DropStorage.WebApi.ServicesDataAccess.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DropStorage.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
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
            return await _userService.Token(access);
        }

        [Authorize]
        [Route("api/auth/user")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<UserDTO> GetUserByUserName(string userName)
        {
            return await _userService.GetUserByName(userName);
        }

        [Authorize]
        [Route("api/user/create")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> CreateUser([FromBody] CreateModifyUserDTO createModifyUserDTO)
        {
            return await _userService.CreateUser(createModifyUserDTO);
        }

        [Authorize]
        [Route("api/user/update")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<UserDTO> UpdateUser(Guid id, [FromBody] CreateModifyUserDTO createModifyUserDTO)
        {
            return await _userService.UpdateUser(id, createModifyUserDTO);
        }

        [Authorize(Roles = "Administrador")]
        [Route("api/user/delete")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> DeleteUser(Guid id)
        {
            string userName = User.Identity.Name;
            return await _userService.DeleteUser(id, userName);
        }

        [AllowAnonymous]
        [Route("api/user/resetpasswordemail")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> ResetPasswordEmail(ResetPasswordEmailDTO email)
        {
            return await _userService.ResetPasswordEmail(email.Email);
        }

        [AllowAnonymous]
        [Route("api/user/resetpassword")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> ResetPassword(ResetPasswordDTO resetPassword)
        {
            return await _userService.ResetPassword(resetPassword);
        }
    }
}
