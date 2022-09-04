using DapperASPNetCore.Dto;
using DapperASPNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Contracts
{

	public interface ICategoryRepository
	{
		public Task<IEnumerable<CategoryDto>> GetCategories();
		public Task<CategoryDto> GetCategory(int id);
		public Task<CategoryDto> CreateCategory(CategoryForCreationDto category);
		public Task UpdateCategory(int id, CategoryUpdateDto category);
		public Task DeleteCategory(int id);

	}
}
