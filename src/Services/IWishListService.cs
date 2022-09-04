using DapperASPNetCore.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DapperASPNetCore.Services
{
    public interface IWishListService
    {
        public Task<IEnumerable<ProductDto>> GetWishList(IdentityDto userClaim);
        Task<int> AddWishList(IdentityDto userClaim, WishListForCreationDto wishlistForCreationDto);        

    }
}