using System.Text.Json.Serialization;

namespace DapperASPNetCore.Dto
{
    public class AddToCartDto
    {

        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public int Id { get; set; }
        
       

        public AddToCartDto()
        {
        }

        public String toString()
        {
            return "CartDto{" +
                    "id=" + Id +
                    ", productId=" + ProductId +
                    ", quantity=" + Quantity +
                    ",";
        }

        public int getId()
        {
            return Id;
        }

        public void setId(int id)
        {
            this.Id = id;
        }


        public int getProductId()
        {
            return ProductId;
        }

        public void SetProductId(int productId)
        {
            this.ProductId = productId;
        }

        public int getQuantity()
        {
            return Quantity;
        }

        public void setQuantity(int quantity)
        {
            this.Quantity = quantity;
        }

    }
}
