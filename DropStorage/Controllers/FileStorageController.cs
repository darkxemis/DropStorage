using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.Services.Services;
using DropStorage.WebApi.ServicesDataAccess.DTOs.FileStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace DropStorage.Controllers
{
    [ApiController]
    public class FileStorageController : ControllerBase
    {
        private readonly FileStorageService _fileStorageService;

        public FileStorageController(FileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        [AllowAnonymous]
        [Route("api/auth/getallfiles")]
        [HttpPost]
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

            var zipName = $"TestFiles-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";
            using (MemoryStream ms = new MemoryStream())
            {
                //required: using System.IO.Compression;
                using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (FileStorage fileStorage in fileStorageList)
                    {
                        //QUery the Products table and get all image content
                        var entry = zip.CreateEntry(fileStorage.Name);
                        using (var entryStream = entry.Open())
                        //using (var fileStream = System.IO.File.OpenRead("C:\\Users\\josemi\\Desktop\\Nueva carpeta\\Prueba.txt"))
                        using (var fileStream = System.IO.File.OpenRead(fileStorage.Url))
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
