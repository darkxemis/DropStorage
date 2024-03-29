﻿
using AutoMapper;
using DropStorage.WebApi.DataModel.Core;
using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.ServicesDataAccess.DTOs;
using DropStorage.WebApi.ServicesDataAccess.DTOs.User;
using Microsoft.Extensions.Logging;

namespace DropStorage.WebApi.ServicesDataAccess.DataAccess
{
    public class UserDataAccess : Service
    {
        private EFUnitOfWork<DropStorageContext> _EF;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserDataAccess(EFUnitOfWork<DropStorageContext> ef, ILogger logger, IMapper mapper)
            : base(logger)
        {
            _EF = ef;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CreateUser(CreateModifyUserDTO createModifyUserDTO)
        {
            bool isSaved = false;

            try
            {
                User user = new User();

                _mapper.Map(createModifyUserDTO, user);

                EFRepository<User> repo = this._EF.Repository<User>();
                repo.Insert(user);

                isSaved = await this._EF.SaveChangesAsync();

            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return isSaved;
        }
    }
}
