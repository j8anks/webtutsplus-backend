namespace Core.Domain.Ecommerce
{
    public class Product : BaseEntity
    {
        public string name { get; set; }
        public string imageURL { get; set; }
        public double price { get; set; }
        public string description { get; set; }
    }
}
