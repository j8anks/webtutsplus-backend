using DapperASPNetCore.Dto;
using DapperASPNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Contracts
{

	public interface ICartRepository
	{
		public Task<int> AddToCart(IdentityDto userClaim, AddToCartDto addToCartDto);
		Task<IEnumerable<CartDto>> CartList(IdentityDto userClaim);
		Task<IEnumerable<CartItemDto>> ListCartItems(IdentityDto userClaim);

		Task<int> DeleteCartItem(IdentityDto userClaim, Int64 cartId);

		// public void ListCartItems(IdentityDto userClaim);

		//public Task<AddToCartDto> GetCartItem(int id);
		//public Task<AddToCartDto> UpdateCartItem(int id, AddToCartDto addToCartDto);
		//public Task DeleteCartItem(int id);
	}
		
}