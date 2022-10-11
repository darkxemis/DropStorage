
using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.ServicesDataAccess.DataAccess;
using DropStorage.WebApi.ServicesDataAccess.DTOs;
using DropStorage.WebApi.ServicesDataAccess.DTOs.User;

namespace DropStorage.WebApi.Services.Services
{
    public class UserService
    {
        private readonly UserDataAccess _userDataAccess;

        public UserService(UserDataAccess userDataAccess)
        {
            this._userDataAccess = userDataAccess;
        }

        public async Task<bool> CreateUser(CreateModifyUserDTO createModifyUserDTO)
        {
            //TODO Create Hash Pasword
            bool isSaved = await _userDataAccess.CreateUser(createModifyUserDTO);

            return isSaved;
        }
    }
}
