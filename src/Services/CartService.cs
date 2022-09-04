
using DapperASPNetCore.Dto;
using DapperASPNetCore.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DapperASPNetCore.Services
{



    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;

        public CartService() { }

        public CartService(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task<int> AddToCart(IdentityDto userClaim, AddToCartDto cartDto)
        {            

            var addToCart = await _cartRepo.AddToCart(userClaim, cartDto);

            return addToCart;
        }

        public async Task<CartDto> CartList(IdentityDto userClaim)
        {
            CartDto cartDto = new CartDto();

            //List<Cart> cartList = cartRepository.findAllByUserOrderByCreatedDateDesc(user);
            //List<CartItemDto> cartItems = new ArrayList<>();
            //for (Cart cart:cartList)
            //{
            //    CartItemDto cartItemDto = getDtoFromCart(cart);
            //    cartItems.add(cartItemDto);
            //}
            //double totalCost = 0;
            //for (CartItemDto cartItemDto :cartItems)
            //{
            //    totalCost += (cartItemDto.getProduct().getPrice() * cartItemDto.getQuantity());
            //}
            return cartDto;
        }

        public async Task<CartDto> ListCartItems(IdentityDto userClaim)
        {

            CartDto cartDto = new CartDto();

            
            var cartItems = await _cartRepo.ListCartItems(userClaim);

            //for (int i = 0; i < cartItems.Count(); i++)
            //{
            //    var elem = cartItems.ElementAt(i);
            //    // do your stuff                   

            //}

            double totalCost = 0;
            foreach (var item in cartItems)
            {             
                
                int quantity = item.Quantity;
                double prodPrice = item.Product.Price;

                double total = quantity * prodPrice;

                totalCost += total;
            }


            

            cartDto.cartItems = cartItems;
            cartDto.TotalCost = totalCost;            

            
            return cartDto;
        }

        public async Task<int> DeleteCartItem(IdentityDto userClaim, Int64 cartId)
        {

            var deleteFromCart = await _cartRepo.DeleteCartItem(userClaim, cartId);

            return deleteFromCart;
        }

        public string AddItem()
        {
            string test = "123";

            return test;
            
        }

    }

}

