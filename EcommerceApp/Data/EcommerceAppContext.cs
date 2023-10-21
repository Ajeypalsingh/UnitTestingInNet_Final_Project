using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.Models;

namespace EcommerceApp.Data
{
    public class EcommerceAppContext : DbContext
    {
        public EcommerceAppContext (DbContextOptions<EcommerceAppContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<Country> Country { get; set; } = null!;
        public DbSet<CartItems> CartItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
    }
}
