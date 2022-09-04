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
	public class CartRepository : ICartRepository
	{
		private readonly DapperContext _context;

		public CartRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<int> AddToCart(IdentityDto userClaim, AddToCartDto addToCartDto)
        {
			var query = @"
			Insert into cart
			  (EntityId, created_date, product_id, quantity, user_id) VALUES (@EntityId, @created_date, @product_id, @quanity, @user_id);";

			var parameters = new DynamicParameters();
			parameters.Add("EntityId", userClaim.EntityId, DbType.String);
			parameters.Add("created_date", DateTime.UtcNow, DbType.DateTime);
			parameters.Add("product_id", addToCartDto.ProductId, DbType.String);
			parameters.Add("quanity", addToCartDto.Quantity, DbType.String);
			parameters.Add("user_id", userClaim.UserId, DbType.String);


			using (var connection = _context.CreateConnection())
			{
				var result = await connection.ExecuteAsync(query, parameters); // .ExecuteScalarAsync<int>

				//var createdCategory = new CategoryDto
				//{
				//	Id = id.ToString(),
				//	Category_name = category.Category_name,
				//	Description = category.Description,
				//	ImageURL = category.ImageURL
				//};

				return result;
			}
		}

		public async Task<IEnumerable<CartDto>> CartList(IdentityDto userClaim)
		{
			var query = @"Select product_id, quantity, created_date from cart Where user_id = @UserId And EntityId = @EntityId ";

			var parameters = new DynamicParameters();
			parameters.Add("EntityId", userClaim.EntityId, DbType.String);			
			parameters.Add("UserId", userClaim.UserId, DbType.String);


			using (var connection = _context.CreateConnection())
			{
				var cartDto = await connection.QueryAsync<CartDto>(query);
				return cartDto.ToList();
			}
		}

        public async Task<IEnumerable<CartItemDto>> ListCartItems(IdentityDto userClaim)
        {
			var query = @"SELECT c.Id
						, c.Quantity
						, c.user_id As UserId
                        , P.Id
						, P.Name                        
                        , P.Price
						, P.ImageUrl
                    FROM Cart AS c
                    INNER JOIN Products AS P On c.product_id = p.id 
					Where c.user_id = @UserId And c.EntityId = @EntityId Order By created_date Desc";

			var parameters = new DynamicParameters();
			parameters.Add("EntityId", userClaim.EntityId, DbType.String);
			parameters.Add("UserId", userClaim.UserId, DbType.String);

			using (var connection = _context.CreateConnection())
			{
				var result = await connection.QueryAsync<CartItemDto, CartProductDto, CartItemDto>(query, (c, p) =>
				{
					c.Product = p;
					return c;
				},
				parameters);				

				return result.AsList();
			}

		}

		public async Task<int> DeleteCartItem(IdentityDto userClaim, Int64 cartId)
		{
			var query = @"Delete From cart Where id = @CartId and EntityId = @EntityId;";

			var parameters = new DynamicParameters();
			parameters.Add("CartId", cartId, DbType.Int64);
			parameters.Add("EntityId", userClaim.EntityId, DbType.String);	

			using (var connection = _context.CreateConnection())
			{
				var result = await connection.ExecuteAsync(query, parameters); // .ExecuteScalarAsync<int>

				return result;
			}
		}

		//      public async void ListCartItems(IdentityDto userClaim)
		//      {
		//	// p.Name, c.quantity, p.price, p.imageUrl



		//}


		//  public async Task<IEnumerable<CartItemDto>> ListCartItems(IdentityDto userClaim)
		//  {
		//      var query = @"Select c.id, p.Id As ProductId, p.Name, c.quantity, p.price, p.imageUrl from cart c
		//Inner Join products p On c.product_id = p.id
		//Where c.user_id = @UserId And c.EntityId = @EntityId ";

		//      var parameters = new DynamicParameters();
		//      parameters.Add("EntityId", userClaim.EntityId, DbType.String);
		//      parameters.Add("UserId", userClaim.UserId, DbType.String);


		//      using (var connection = _context.CreateConnection())
		//      {
		//          var cartItemDto = await connection.QueryAsync<CartItemDto>(query, parameters);

		//          return cartItemDto.ToList();
		//      }
		//  }

		//	CartItemDto cartItemDto = new CartItemDto();

		//	// SELECT Id, tstamp As Name, pressure As Address, temperature as Country

		//	var query = @"Select c.id, p.Name, c.quantity, p.price, p.imageUrl from cart c
		//       				Inner Join products p On c.product_id = p.id
		//       				  ";

		//           // var parameters = new DynamicParameters();
		//           //parameters.Add("Page", pager.Offset, DbType.Int32);
		//           //parameters.Add("PageSize", pager.PageSize, DbType.Int32);

		//           // GetCompanies1();

		//           using (var connection = _context.CreateConnection())
		//           {
		//               var companies = await connection.QueryAsync<CartItemDto>(query);
		////return companies.ToList();
		// }



		//public Task<AddToCartDto> GetCartItem(int id)
		//{

		//}

		//public Task<AddToCartDto> UpdateCartItem(int id, AddToCartDto addToCartDto)
		//{

		//}

		//public Task DeleteCartItem(int id)
		//{

		//}

	}
}
