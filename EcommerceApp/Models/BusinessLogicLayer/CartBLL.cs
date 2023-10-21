﻿using EcommerceApp.Data;

namespace EcommerceApp.Models.BusinessLogicLayer
{
    public class CartBLL
    {

        private ICartRepository<Cart> _cartRepository;

        public CartBLL( ICartRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void RemoveProductFromCart(Guid productId)
        {
            _cartRepository.RemoveFromCart(productId);
        }

        public ICollection<CartItems> GetAllCartItems()
        {
            return _cartRepository.GetAllCartItem();
        }

        // It should have method to show sum of total price in items in cart 

        public decimal GetCartPrice()
        {
             return _cartRepository.SumOfAllItemPriceInCart();
        }

        public ICollection<Country> GetAllCountries()
        {
            return _cartRepository.GetAllCountries();
        }

        public Country GetCountry(int? id)
        {
            return _cartRepository.GetCountry(id);
        }

        public void AddOrder(Order order)
        {
            _cartRepository.AddOrder(order);
        }

        public ICollection <Order> GetAllOrders()
        {
            return _cartRepository.GetAllOrders();
        }

        public void ClearCart()
        {
            _cartRepository.ClearCart();
        }
    }
}
