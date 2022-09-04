using DapperASPNetCore.Dto;
using DapperASPNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Contracts
{
	public interface IWishListRepository
	{
		public Task<IEnumerable<WishListDto>> GetWishList(IdentityDto userClaim);
		Task<int> AddToWishList(IdentityDto userClaim, WishListForCreationDto wishlistForCreationDto);
		Task<int> RemoveFromWishList(IdentityDto userClaim, long wishListId);
	}
}