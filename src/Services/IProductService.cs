using Core.Domain.Ecommerce;
using DapperASPNetCore.Dto; 

namespace DapperASPNetCore.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProductById(int id);
    }
}