using Core.Domain.Ecommerce;
using DapperASPNetCore.Dto;
using DapperASPNetCore.Repository;

namespace DapperASPNetCore.Services
{



    public class ProductService : IProductService
    {
        private ProductRepository productRepository;

        public ProductService() { }

        public ProductService(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
               

        Task<ProductDto> IProductService.GetProductById(int id)
        {
            var product = productRepository.GetProduct(id);

            return product;
        }
    }

}

