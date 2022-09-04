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
	public class CategoryRepository : ICategoryRepository
	{
		private readonly DapperContext _context;

		public CategoryRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<CategoryDto>> GetCategories()
		{


			var query = @"
				SELECT c.Id, c.category_name as CategoryName, c.description, c.image_url As ImageURL,
						p.Id, p.description, p.Name, P.Price, P.ImageUrl, P.category_id As CategoryId 
				FROM Categories c
				LEFT JOIN Products p ON c.id = p.category_id ";
				// Where c.EntityId = @EntityId Order by c.category_name ";

			//var parameters = new DynamicParameters();
			//parameters.Add("EntityId", userClaim.EntityId, DbType.String);

			using (var connection = _context.CreateConnection())
			{

				var lookup = new Dictionary<int, CategoryDto>();
				connection.Query<CategoryDto, ProductDto, CategoryDto>(query, (c, p) => {
								CategoryDto categories;
								if (!lookup.TryGetValue(c.Id, out categories))
									lookup.Add(c.Id, categories = c);
								if (categories.Products == null)
								categories.Products = new List<ProductDto>();
								categories.Products.Add(p); /* Add products to categories */
								return categories;
							}).AsQueryable();
							var resultList = lookup.Values;
				return resultList;

			}

			

			

			//  var query = @"Select id, category_name as CategoryName, description, image_url As ImageURL FROM categories Order By category_name ";


			//using (var connection = _context.CreateConnection())
			//{
			//	var categories = await connection.QueryAsync<CategoryDto>(query);
			//	return categories.ToList();
			//}
		}

		public async Task<CategoryDto> GetCategory(int id)
		{
			var query = "SELECT Id, category_name as CategoryName, description, image_url As imageURL FROM categories WHERE Id = @Id";

			using (var connection = _context.CreateConnection())
			{
				var category = await connection.QuerySingleOrDefaultAsync<CategoryDto>(query, new { id });

				return category;
			}
		}


		public async Task<CategoryDto> CreateCategory(CategoryForCreationDto category)
		{

			var query = @"
			Insert into categories
			  (EntityId, category_name, description, image_url) VALUES (@EntityId, @Category_name, @Description, @Image_url);

			select LAST_INSERT_ID();";

			var parameters = new DynamicParameters();
			parameters.Add("EntityId", "123456", DbType.String);			
			parameters.Add("Category_name", category.CategoryName, DbType.String);
			parameters.Add("Description", category.Description, DbType.String);
			parameters.Add("Image_url", category.ImageURL, DbType.String);


			using (var connection = _context.CreateConnection())
			{
				var id = await connection.ExecuteScalarAsync<int>(query, parameters);

				var createdCategory = new CategoryDto
				{
					Id = id,
					CategoryName = category.CategoryName,
					Description = category.Description,
					ImageURL = category.ImageURL
				};

				return createdCategory;
			}
		}

		public async Task UpdateCategory(int id, CategoryUpdateDto category)
		{
			var query = "UPDATE categories SET category_name = @Category_name, description = @Description, image_url = @Image_url WHERE Id = @Id";

			var parameters = new DynamicParameters();
			parameters.Add("Id", id, DbType.Int32);
			parameters.Add("Category_name", category.CategoryName, DbType.String);
			parameters.Add("Description", category.Description, DbType.String);
			parameters.Add("Image_url", category.ImageURL, DbType.String);

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameters);
			}
		}

		public async Task DeleteCategory(int id)
		{
			var query = "DELETE FROM categories WHERE Id = @Id";

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, new { id });
			}
		}

	}
}
