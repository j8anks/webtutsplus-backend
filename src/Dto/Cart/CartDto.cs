using Core.Domain.Ecommerce;

namespace DapperASPNetCore.Dto
{
    public class CartDto
    {

        public IEnumerable<CartItemDto>? cartItems { get; set; } 
        public double TotalCost { get; set; }        
        

    }  
}
