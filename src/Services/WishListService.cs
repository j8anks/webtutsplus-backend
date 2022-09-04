
using DapperASPNetCore.Dto;
using DapperASPNetCore.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DapperASPNetCore.Services
{


    public class WishListService : IWishListService
    {
        private readonly IWishListRepository _wishListRepo;
        private readonly IProductRepository _productRepo;

        public WishListService() { }

        public WishListService(IWishListRepository wishListRepo, IProductRepository productRepo)
        {
            _wishListRepo = wishListRepo;
            _productRepo = productRepo;
        }

        public async Task<IEnumerable<ProductDto>> GetWishList(IdentityDto userClaim)
        {
            var wishList = await _wishListRepo.GetWishList(userClaim);
            var productDto = await GetDtoFromProduct(wishList);

            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetDtoFromProduct(IEnumerable<WishListDto> wishList)
        {
            List<ProductDto> products = new List<ProductDto>();


            foreach (var item in wishList)
            {
                var product =  await CreateProductDtoItem(item.Product);

                products.Add(product);
            }

            return products;
        }

        private async Task<ProductDto>  CreateProductDtoItem(long productId)
        {
            var product = await _productRepo.GetProduct(productId);

            return product;

        }

        public async Task<int> AddWishList(IdentityDto userClaim, WishListForCreationDto wishlistForCreationDto)
        {

            var addToWishList = await _wishListRepo.AddToWishList(userClaim, wishlistForCreationDto);

           return addToWishList;
        }

       

    }

}

