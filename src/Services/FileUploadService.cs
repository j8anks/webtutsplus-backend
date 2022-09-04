using Core.Domain.Ecommerce;
using DapperASPNetCore.Dto;
using DapperASPNetCore.Repository;
using DapperASPNetCore.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Globalization;
using System.Drawing;

// https://code-maze.com/upload-files-dot-net-core-angular/
// https://stacktuts.com/how-to-convert-iformfile-to-byte-array-c

namespace DapperASPNetCore.Services
{
    public class FileUploadService : IFileUploadService
    {
        private string sortableDateTimeFormat = DateTimeFormatInfo.InvariantInfo.SortableDateTimePattern;

        private readonly IFileUploadRepository _fileUploadRepo;
        public FileUploadService(IFileUploadRepository fileUploadRepo)
        {
            _fileUploadRepo = fileUploadRepo;
        }


        public Task<string> StoreFile(IFormFile? file)
        {
            try
            {
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    byte[] uploadArray = null;
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // Uncomment the following to save to database. Not recommended;

                    //if (file.Length > 0)
                    //{
                    //   using (var ms = new MemoryStream())
                    //   {
                    //       file.CopyTo(ms);
                    //       var fileBytes = ms.ToArray();
                    //       //string s = Convert.ToBase64String(fileBytes);
                    //       // act on the Base64 data
                    //       uploadArray = fileBytes;
                    //   }
                    //}


                    StoreFileToDb(file, dbPath.ToString(), uploadArray);

                    return Task.FromResult(dbPath.ToString());
                }
                else
                {
                    return Task.FromResult("BadRequest");
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult("Exception " + ex.ToString());
            }
        }

        public Task<string> StoreFileToDb(IFormFile? file, string imageUrl, byte[] uploadArray)
        {
            try
            {

                if (file.Length > 0)
                {

                    FileUploadDto fileUpload = new FileUploadDto();


                    fileUpload.FileName = file.FileName;
                    fileUpload.FileSize = file.Length;
                    fileUpload.FileContent = uploadArray;
                    fileUpload.ImageUrl = imageUrl;
                    fileUpload.Created = DateTime.UtcNow.ToString(sortableDateTimeFormat);
                    fileUpload.CreatedBy = "jbanks";

                    var result = _fileUploadRepo.StoreUpload(fileUpload, uploadArray);

                    return Task.FromResult(result.ToString());


                }
                else
                {
                    return Task.FromResult("BadRequest");
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult("Exception " + ex.ToString());
            }
        }
        public async Task<List<FilesListDto>> GetListFiles()
        {
            var files = await _fileUploadRepo.GetListFiles();

            return files;

        }
    }
}
