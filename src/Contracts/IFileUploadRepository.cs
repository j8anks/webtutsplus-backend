using WebApplication.Models;
using DapperASPNetCore.Dto;
namespace DapperASPNetCore.Contracts
{

	public interface IFileUploadRepository
	{		
		public Task<int> StoreUpload(FileUploadDto file, byte[] uploadArray);
		Task<List<FilesListDto>> GetListFiles();



	}
}
