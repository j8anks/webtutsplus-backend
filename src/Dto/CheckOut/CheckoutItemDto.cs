namespace DapperASPNetCore.Dto
{
    public class CheckoutItemDto
    {
        public string productName { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public long productId { get; set; }
        public int userId { get; set; }


    }
}
