
using AutoMapper;
using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.DataModel.Security;
using DropStorage.WebApi.Services.Constants;
using DropStorage.WebApi.Services.Exceptions;
using DropStorage.WebApi.Services.Services.AuthServices;
using DropStorage.WebApi.ServicesDataAccess.DataAccess;
using DropStorage.WebApi.ServicesDataAccess.DTOs;
using DropStorage.WebApi.ServicesDataAccess.DTOs.Auth;
using DropStorage.WebApi.ServicesDataAccess.DTOs.User;
using System.Net;
using System.Security.Claims;

namespace DropStorage.WebApi.Services.Services
{
    public class UserService
    {
        private readonly UserDataAccess _userDataAccess;
        private JwtTokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(UserDataAccess userDataAccess, JwtTokenService tokenService, IMapper mapper)
        {
            _userDataAccess = userDataAccess;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<AuthDTO> Token(AccessDTO access)
        {
            User? user = await _userDataAccess.GetByUsername(access.username);

            if (user == null || !Hasher.VerifyIdentityV3Hash(access.password, user.Password))
            {
                throw new HttpStatusException(HttpStatusCode.Unauthorized, "User name or password wrong!");
            }

            List<Claim> userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, (user.Rol != null ? user.Rol.Description : "Usuario"))
            };

            string accessToken = this._tokenService.GenerateAccessToken(userClaims);
            //string refreshToken = this.tokenService.GenerateRefreshToken();

            return new AuthDTO()
            {
                access_token = accessToken,
                //refresh_token = refreshToken,
            };
        }

        public async Task<bool> CreateUser(CreateModifyUserDTO createModifyUserDTO)
        {
            User userToSave = new User();

            //Create pass with hash
            string password = Hasher.GenerateIdentityV3Hash(createModifyUserDTO.Password);

            _mapper.Map(createModifyUserDTO, userToSave);
            userToSave.Password = password;

            bool isSaved = await _userDataAccess.CreateUser(userToSave);

            return isSaved;
        }

        public async Task<UserDTO> UpdateUser(Guid id, CreateModifyUserDTO createModifyUserDTO)
        {

            User? userToModify = await _userDataAccess.GetUserById(id);

            if (userToModify == null)
            {
                throw new HttpStatusException(HttpStatusCode.Forbidden, "User not found");
            }

            _mapper.Map(createModifyUserDTO, userToModify);

            string passwordInDTO = Hasher.GenerateIdentityV3Hash(createModifyUserDTO.Password);
            if (passwordInDTO != userToModify.Password)
            {
                userToModify.Password = passwordInDTO;
            }

            bool isSaved = await _userDataAccess.UpdateUser(userToModify);

            if (!isSaved)
            {
                throw new HttpStatusException(HttpStatusCode.Forbidden, "User couldt'n be update");
            }

            userToModify = await _userDataAccess.GetUserById(id);
            UserDTO userDTO = new UserDTO();
            _mapper.Map(userToModify, userDTO);

            return userDTO;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            User? userToDetele = await _userDataAccess.GetUserById(id);

            if (userToDetele == null)
            {
                throw new HttpStatusException(HttpStatusCode.Forbidden, "User not found");
            }

            bool isDeleted = await _userDataAccess.DeleteUser(userToDetele);

            return isDeleted;
        }
    }
}
