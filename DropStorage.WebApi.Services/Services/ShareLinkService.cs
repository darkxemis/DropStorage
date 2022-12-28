using AutoMapper;
using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.Services.Exceptions;
using DropStorage.WebApi.Services.Extensions;
using DropStorage.WebApi.Services.Services.EmailServices;
using DropStorage.WebApi.ServicesDataAccess.DataAccess;
using DropStorage.WebApi.ServicesDataAccess.DTOs;
using DropStorage.WebApi.ServicesDataAccess.DTOs.ShareLink;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace DropStorage.WebApi.Services.Services
{
    public class ShareLinkService
    {
        private readonly ShareLinkDataAccess _shareLinkDataAccess;
        private readonly UserService _userService;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ShareLinkService(ShareLinkDataAccess shareLinkDataAccess, UserService userService, EmailService emailService, IMapper mapper, IConfiguration configuration)
        {
            _shareLinkDataAccess = shareLinkDataAccess;
            _mapper = mapper;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<ShareLinkDTO> CreateShareLinkAsync(string userName, CreateShareLinkDTO createShareLinkDTO)
        {
            List<ShareLinkFileStorage> shareLinkFileStorageList = new List<ShareLinkFileStorage>();
            ShareLinkDTO shareLinkDTO = new ShareLinkDTO();

            UserDTO activeUser = await _userService.GetUserByName(userName);

            //ShareLink
            ShareLink shareLink = new ShareLink()
            {
                ExpirationDate = DateTime.Now.AddDays(1),
                UserId = activeUser.Id
            };
            await _shareLinkDataAccess.CreateShareLink(shareLink, createShareLinkDTO.idsFileStorage);

            await this.SendEmailAsync(shareLink.Id, createShareLinkDTO.emails, activeUser.Name);

            return _mapper.Map(shareLink, shareLinkDTO);
        }

        public async Task<List<ShareLinkFileStorage>> DownloadSharedFileAsync(Guid idShareLink)
        {
            return await _shareLinkDataAccess.GetShareLinkFileStorageById(idShareLink);
        }

        private async Task SendEmailAsync(Guid id, List<string> emails, string userName)
        {
            string url = string.Format("{0}public/shared-link-file/{1}", _configuration.GetUrlWeb(), id);
            string subject = "Share file";
            string buttomLink = string.Format("<a href='{0}'>Share File Link</a>", url);
            string body = string.Format("The user {0} shared you a document. To download file click in next link: {1}", userName, buttomLink);

            bool isSended = await _emailService.SendMessageAsync(body, subject, emails);
        }
    }
}
