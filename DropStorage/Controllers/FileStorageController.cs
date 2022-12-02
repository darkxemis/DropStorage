using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.Services.Exceptions;
using DropStorage.WebApi.Services.Services;
using DropStorage.WebApi.ServicesDataAccess.DTOs;
using DropStorage.WebApi.ServicesDataAccess.DTOs.FileStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Net;

namespace DropStorage.Controllers
{
    [ApiController]
    public class FileStorageController : ControllerBase
    {
        private readonly FileStorageService _fileStorageService;
        private readonly UserService _userService;

        public FileStorageController(FileStorageService fileStorageService, UserService userService)
        {
            _fileStorageService = fileStorageService;
            _userService = userService;
        }

        [AllowAnonymous]
        [Route("api/auth/getallfiles")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<FileStorageDTO>> GetAllFilesByUserId(Guid idUser)
        {
            return await _fileStorageService.GetAllFilesByUserId(idUser);
        }

        [AllowAnonymous]
        [Route("api/auth/downloadfile")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DownloadFile(List<Guid> ids)
        {
            List<FileStorage> fileStorageList = await _fileStorageService.GetInfoFiles(ids);

            var zipName = $"{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";
            using (MemoryStream ms = new MemoryStream())
            {
                using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (FileStorage fileStorage in fileStorageList)
                    {
                        var entry = zip.CreateEntry(fileStorage.Name);
                        using (var entryStream = entry.Open())
                        using (var fileStream = System.IO.File.OpenRead(fileStorage.Url))
                        {
                            await fileStream.CopyToAsync(entryStream);
                        }
                    }
                }

                return this.File(ms.ToArray(), "application/zip", zipName);
            }
        }

        [AllowAnonymous]
        [Route("api/auth/uploadfiles")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> UploadFile([FromForm] IFormFile file)
        {
            string userName = User.Identity.Name;
            UserDTO activeUser = await _userService.GetUserByName(userName);

            string filePath = Path.Combine(activeUser.DirectoryHome, file.FileName);
            if (System.IO.File.Exists(filePath))
            {
                throw new HttpStatusException(HttpStatusCode.Forbidden, "File already exist in the system");
            }
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            FileStorage newFileStorage = new FileStorage()
            {
                CreateTime = DateTime.Now,
                Name = file.FileName,
                Extension = Path.GetExtension(file.FileName),
                SizeBytes = file.Length,
                Url = string.Format("{0}{1}", activeUser.DirectoryHome, file.FileName),
                UserId = activeUser.Id
            };

            bool isSaved = await _fileStorageService.InsertDropStorageFile(newFileStorage);

            return isSaved;
        }
    }
}
