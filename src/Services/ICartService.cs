using DapperASPNetCore.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DapperASPNetCore.Services
{
    public interface ICartService
    {
        public string AddItem();
        Task<int> AddToCart(IdentityDto userClaim, AddToCartDto cartDto);
        Task<CartDto> CartList(IdentityDto userClaim);
        Task<CartDto> ListCartItems(IdentityDto userClaim);
        Task<int> DeleteCartItem(IdentityDto userClaim, Int64 cartId);
    }
}
