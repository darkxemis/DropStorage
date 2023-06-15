using AutoMapper;
using DropStorage.WebApi.Services.Exceptions;
using DropStorage.WebApi.Services.Extensions.ModelConfiguration;
using DropStorage.WebApi.Services.Services;
using DropStorage.WebApi.ServicesDataAccess.DTOs;
using DropStorage.WebApi.ServicesDataAccess.DTOs.Auth;
using DropStorage.WebApi.ServicesDataAccess.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace DropStorage.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        private readonly EmailConfiguration _emailConfiguration;

        public UserController(UserService userService, IMapper mapper, IOptions<EmailConfiguration> emailConfiguration)
        {
            _userService = userService;
            _mapper = mapper;
            _emailConfiguration = emailConfiguration.Value;
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
        [Route("api/user/prueba")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public EmailConfiguration Prueba()
        {
            return _emailConfiguration;
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

        [Authorize]
        [Route("api/user/getImg")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetImg(Guid id)
        {
            byte[] imgByte = await this._userService.GetImg(id);

            if (imgByte == null)
            {
                return Ok(imgByte);
            } else
            {
                return File(imgByte, "image/jpg");
            }
        }

        [Authorize]
        [Route("api/user/uploadimg")]
        [HttpPost]
        [RequestSizeLimit(10000000)]
        //[DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> UploadFile([FromForm] IFormFile file)
        {
            string userName = User.Identity.Name;
            UserDTO activeUser = await _userService.GetUserByName(userName);

            string newFilePath = Path.Combine(activeUser.DirectoryHome, file.FileName);

            if (activeUser.ProfilePhotoUrl == newFilePath)
            {
                return true;
            }

            if (System.IO.File.Exists(activeUser.ProfilePhotoUrl))
            {
                System.IO.File.Delete(activeUser.ProfilePhotoUrl);
            }

            using (Stream fileStream = new FileStream(newFilePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            CreateModifyUserDTO createModifyUserDTO = new CreateModifyUserDTO();
            _mapper.Map(activeUser, createModifyUserDTO);
            createModifyUserDTO.ProfilePhotoUrl = newFilePath;

            //CreateModifyUserDTO createModifyUserDTO = new CreateModifyUserDTO()
            //{
            //    Address = activeUser.Address,
            //    Login = activeUser.Login,
            //    ProfilePhotoUrl = newFilePath,
            //    DirectoryHome = activeUser.DirectoryHome,
            //    LastName = activeUser.LastName,
            //    Name = activeUser.Name,
            //    Password = activeUser.Password,
            //    RolId = activeUser.RolId,
            //};
            await this._userService.UpdateUser(activeUser.Id, createModifyUserDTO);

            return true;
        }
    }
}
