using Core.Domain.Users;
namespace Core.Domain.Ecommerce
{
    public class Cart : BaseEntity
    {
        
        private DateTime createdDate;
        private Product product;
        private User user;


        private int quantity;

        public Cart()
        {
        }

        public Cart(Product product, int quantity, User user)
        {
            this.user = user;
            this.product = product;
            this.quantity = quantity;
            this.createdDate = new DateTime();
        }

        public int getId()
        {
            return Id;
        }

        public void setId(int id)
        {
            this.Id = id;
        }

        public User getUser()
        {
            return user;
        }

        public void setUser(User user)
        {
            this.user = user;
        }

        public DateTime getCreatedDate()
        {
            return createdDate;
        }

        public void setCreatedDate(DateTime createdDate)
        {
            this.createdDate = createdDate;
        }

        public Product getProduct()
        {
            return product;
        }

        public void setProduct(Product product)
        {
            this.product = product;
        }

        public int getQuantity()
        {
            return quantity;
        }

        public void setQuantity(int quantity)
        {
            this.quantity = quantity;
        }
    }

}

