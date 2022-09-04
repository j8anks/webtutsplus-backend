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
	public class WishListRepository : IWishListRepository
	{
		private readonly DapperContext _context;

		public WishListRepository(DapperContext context)
		{
			_context = context;
		}


		public async Task<IEnumerable<WishListDto>> GetWishList(IdentityDto userClaim)
		{
			var query = @"SELECT EntityId, created_date As CreatedDate, product_id as Product, user_id As User FROM wishlist
			Where EntityId = @EntityId and user_id = @UserId";

			var parameters = new DynamicParameters();
			parameters.Add("EntityId", userClaim.EntityId, DbType.String);
			parameters.Add("UserId", userClaim.UserId, DbType.String);


			using (var connection = _context.CreateConnection())
			{
				var wishListDto = await connection.QueryAsync<WishListDto>(query, parameters);
				return wishListDto.ToList();
			}
		}


		

		public async Task<int> AddToWishList(IdentityDto userClaim, WishListForCreationDto wishlistForCreationDto)
		{
			var query = @"
			Insert into wishlist 
			  (EntityId, created_date, product_id, user_id) VALUES (@EntityId, @created_date, @product_id, @user_id); 
				select LAST_INSERT_ID();";

			var parameters = new DynamicParameters();
			parameters.Add("EntityId", userClaim.EntityId, DbType.String);
			parameters.Add("created_date", DateTime.UtcNow, DbType.DateTime);
			parameters.Add("product_id", wishlistForCreationDto.Id, DbType.String);
			parameters.Add("user_id", userClaim.UserId, DbType.String);

			using (var connection = _context.CreateConnection())
			{
				var id = await connection.ExecuteScalarAsync<int>(query, parameters);							

				return id;
			}
		}

		public async Task<int> RemoveFromWishList(IdentityDto userClaim, long wishListId)
		{
			var query = @"Delete From wishlist Where id = @WishListId And user_id = @UserId and EntityId = @EntityId;";

			var parameters = new DynamicParameters();
			parameters.Add("WishListId", wishListId, DbType.Int64);
			parameters.Add("UserId", userClaim.UserId, DbType.String);
			parameters.Add("EntityId", userClaim.EntityId, DbType.String);

			using (var connection = _context.CreateConnection())
			{
				var result = await connection.ExecuteAsync(query, parameters); 

				return result;
			}
		}        
	}
		
}
