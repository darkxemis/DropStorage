using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.Services.Exceptions;
using DropStorage.WebApi.Services.Services;
using DropStorage.WebApi.ServicesDataAccess.DTOs.ShareLink;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Net;

namespace DropStorage.Controllers
{
    [ApiController]
    public class ShareLinkController : ControllerBase
    {
        private readonly ShareLinkService _shareLinkService;

        public ShareLinkController(ShareLinkService shareLinkService)
        {
            _shareLinkService = shareLinkService;
        }

        [Authorize]
        [Route("api/auth/createsharelink")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ShareLinkDTO> CreateShareLink(CreateShareLinkDTO createShareLinkDTO)
        {
            string userName = User.Identity.Name;
            return await _shareLinkService.CreateShareLinkAsync(userName, createShareLinkDTO);
        }

        [Route("api/public/downloadsharedfile")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShareLink(IdShareLink idShareLink)
        {
            List<ShareLinkFileStorage> shareLinkFileStorageList = await _shareLinkService.DownloadSharedFileAsync(idShareLink.idShareLink);

            if(shareLinkFileStorageList.FirstOrDefault()?.IdShareLinkNavigation.ExpirationDate < DateTime.Now)
            {
                throw new HttpStatusException(HttpStatusCode.Unauthorized, "The date expired");
            }

            var zipName = $"{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";
            using (MemoryStream ms = new MemoryStream())
            {
                using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (ShareLinkFileStorage shareLinkFileStorage in shareLinkFileStorageList)
                    {
                        var entry = zip.CreateEntry(shareLinkFileStorage.IdFileStorageNavigation.Name);
                        using (var entryStream = entry.Open())
                        using (var fileStream = System.IO.File.OpenRead(shareLinkFileStorage.IdFileStorageNavigation.Url))
                        {
                            await fileStream.CopyToAsync(entryStream);
                        }
                    }
                }

                return this.File(ms.ToArray(), "application/zip", zipName);
            }
        }
    }
}
