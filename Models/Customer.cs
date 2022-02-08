using System;
using System.Collections.Generic;

#nullable disable

namespace HockeyShop1.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
            ShoppinCarts = new HashSet<ShoppinCart>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ShoppinCart> ShoppinCarts { get; set; }
    }
}
