using Dapper;
using DapperASPNetCore.Context;
using DapperASPNetCore.Contracts;
using DapperASPNetCore.Dto;
using DapperASPNetCore.Entities;
using WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Repository
{
	public class FileUploadRepository : IFileUploadRepository
	{
		private readonly DapperContext _context;

		public FileUploadRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<int> StoreUpload(FileUploadDto file, byte[] uploadArray)
		{

			var query = @"Insert Into uploads 
						(FileName, FileSize, FileContent, ImageUrl, Created, CreatedBy) 
						values(@FileName, @FileSize, @FileContent, @ImageUrl, @Created, @CreatedBy)";

			var parameters = new DynamicParameters();
			parameters.Add("FileName", file.FileName, DbType.String);
			parameters.Add("FileSize", file.FileSize, DbType.String);
			parameters.Add("FileContent", uploadArray);
			parameters.Add("ImageUrl", file.ImageUrl, DbType.String);
			parameters.Add("Created", file.Created, DbType.String);
			parameters.Add("CreatedBy", file.CreatedBy, DbType.String);

			using (var connection = _context.CreateConnection())
			{
				try
				{
					var result = await connection.ExecuteScalarAsync<int>(query, parameters);

					return result;
				}
				catch (Exception ex)
				{
					string message = ex.Message;
					return 2;
				}
			}
		}

		public async Task<List<FilesListDto>> GetListFiles()
		{
			var query = @"SELECT FileName as name, ImageUrl as url FROM uploads   ";
					
			using (var connection = _context.CreateConnection())
			{
				var files = await connection.QueryAsync<FilesListDto>(query);
				return files.ToList();
			}
		}
	}
}
