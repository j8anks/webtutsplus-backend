using DapperASPNetCore.Dto;
using DapperASPNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Contracts
{

	public interface IProductRepository
	{
		public Task<IEnumerable<ProductDto>> GetProducts();
		public Task<ProductDto> GetProduct(long id);
		public Task<ProductDto> CreateProduct(ProductForCreationDto product);
		public Task UpdateProduct(int id, ProductUpdateDto product);
		public Task DeleteCompany(int id);

	}
}
