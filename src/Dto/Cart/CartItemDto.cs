namespace DapperASPNetCore.Dto
{
    public class CartItemDto
    {
        public Int64 Id { get; set; }       
        public int  Quantity { get; set; }        
        public CartProductDto? Product { get; set; }
        public int UserId { get; set; }

    }
}
