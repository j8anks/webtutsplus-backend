namespace DapperASPNetCore.Dto
{
    public class OrderItemsDto
    {
        double price { get; set; }
        int quantity { get; set; }
        int orderId { get; set; }
        int productId { get; set; }

    }
}
