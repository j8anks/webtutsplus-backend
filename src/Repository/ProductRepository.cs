using Dapper;
using DapperASPNetCore.Context;
using DapperASPNetCore.Contracts;
using DapperASPNetCore.Dto;
using DapperASPNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly DapperContext _context;

		public ProductRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<ProductDto>> GetProducts()
		{			

			var query = @"SELECT id, name, imageURL, price, description, category_id As CategoryId FROM products ";

			//var parameters = new DynamicParameters();
			//parameters.Add("Page", pager.Offset, DbType.Int32);
			//parameters.Add("PageSize", pager.PageSize, DbType.Int32);
			//GetProductPageCount();

			using (var connection = _context.CreateConnection())
			{
				var products = await connection.QueryAsync<ProductDto>(query);
				return products.ToList();
			}
		}

		public async Task<ProductDto> GetProduct(long id)
		{
			var query = "SELECT name, description, imageurl, price, category_id As CategoryId FROM products WHERE Id = @Id";

			using (var connection = _context.CreateConnection())
			{
				var product = await connection.QuerySingleOrDefaultAsync<ProductDto>(query, new { id });

				return product;
			}
		}


		public async Task<ProductDto> CreateProduct(ProductForCreationDto product)
		{

			var query = @"
			Insert into products
			  (EntityId, name, description, imageurl, price, category_id) VALUES (@EntityId, @name, @description, @imageurl, @price, @category_id);

			select LAST_INSERT_ID();";

			var parameters = new DynamicParameters();
			parameters.Add("EntityId", "123456", DbType.String);
			parameters.Add("name", product.Name, DbType.String);
			parameters.Add("description", product.Description, DbType.String);
			parameters.Add("imageurl", product.ImageURL, DbType.String);			
			parameters.Add("price", product.Price, DbType.String);
			parameters.Add("category_id", product.CategoryId, DbType.String);

			using (var connection = _context.CreateConnection())
			{
				var id = await connection.ExecuteScalarAsync<int>(query, parameters);

				var createdProduct = new ProductDto
				{
					Id = id.ToString(),
					Name = product.Name,
					Description = product.Description,
					Price = product.Price,
					ImageURL = product.ImageURL
				};

				return createdProduct;
			}
		}

		public async Task UpdateProduct(int id, ProductUpdateDto product)
		{
			var query = "UPDATE products SET name = @name, description = @description, imageurl = @imageurl, price = @price, category_id = @category_id WHERE Id = @Id";

			var parameters = new DynamicParameters();
			parameters.Add("Id", id, DbType.Int32);
			parameters.Add("name", product.Name, DbType.String);
			parameters.Add("description", product.Description, DbType.String);
			parameters.Add("imageurl", product.ImageURL, DbType.String);
			parameters.Add("price", product.Price, DbType.String);
			parameters.Add("category_id", product.CategoryId, DbType.String);

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameters);
			}
		}

		public async Task DeleteCompany(int id)
		{
			var query = "DELETE FROM products WHERE Id = @Id";

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, new { id });
			}
		}

	}
}
