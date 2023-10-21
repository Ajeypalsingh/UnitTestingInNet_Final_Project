namespace EcommerceApp.Models
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public List<CartItems> CartItems { get; set; } = new List<CartItems>();

    }
}
