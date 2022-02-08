using System;
using System.Collections.Generic;

#nullable disable

namespace HockeyShop1.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ShoppinCarts = new HashSet<ShoppinCart>();
        }

        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string ModelName { get; set; }
        public string Color { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ShoppinCart> ShoppinCarts { get; set; }
    }
}
