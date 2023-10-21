namespace EcommerceApp.Models
{
    public class CartItems
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public Cart Cart { get; set; }
        public Guid CartId { get; set; }
    }
}
