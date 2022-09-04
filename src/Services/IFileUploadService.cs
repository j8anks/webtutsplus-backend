using Core.Domain.Ecommerce;
using DapperASPNetCore.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DapperASPNetCore.Services
{
    public interface IFileUploadService
    {
        Task<string> StoreFile(IFormFile? file);
        Task<List<FilesListDto>> GetListFiles();

    }
}
