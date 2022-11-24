
using AutoMapper;
using DropStorage.WebApi.DataModel.Core;
using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.ServicesDataAccess.DTOs;
using DropStorage.WebApi.ServicesDataAccess.DTOs.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DropStorage.WebApi.ServicesDataAccess.DataAccess
{
    public class UserDataAccess : Service
    {
        private EFUnitOfWork<DropStorageContext> _EF;
        private readonly ILogger _logger;

        public UserDataAccess(EFUnitOfWork<DropStorageContext> ef, ILogger logger)
            : base(logger)
        {
            _EF = ef;
            _logger = logger;
        }

        public async Task<User?> GetByUsername(string username)
        {
            EFRepository<User> repo = _EF.Repository<User>();
            User? user = await repo.Query()
                .Include(user => user.Rol)
                .FirstOrDefaultAsync(user => user.Login.ToLower() == username.ToLower());
            return user;
        }

        public async Task<User?> GetUserById(Guid id)
        {
            EFRepository<User> repo = _EF.Repository<User>();
            User? user = await repo.Query()
                .Where(user => user.Id == id)
                .Include(user => user.Rol)
                .FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            EFRepository<User> repo = _EF.Repository<User>();
            repo.Update(user);
            bool isSaved = await _EF.SaveChangesAsync();
            return isSaved;
        }

        public async Task<bool> CreateUser(User user)
        {
            bool isSaved = false;

            EFRepository<User> repo = this._EF.Repository<User>();
            repo.Insert(user);

            isSaved = await this._EF.SaveChangesAsync();

            return isSaved;
        }

        public async Task<bool> UpdateUser(User user)
        {
            bool isSaved = false;

            EFRepository<User> repo = this._EF.Repository<User>();
            repo.Update(user);

            isSaved = await this._EF.SaveChangesAsync();

            return isSaved;
        }


        public async Task<bool> DeleteUser(User user)
        {
            bool isDeleted = false;

            EFRepository<User> repo = this._EF.Repository<User>();
            repo.Delete(user);

            isDeleted = await this._EF.SaveChangesAsync();

            return isDeleted;
        }
    }
}
