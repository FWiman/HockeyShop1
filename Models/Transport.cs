using System;
using System.Collections.Generic;

#nullable disable

namespace HockeyShop1.Models
{
    public partial class Transport
    {
        public Transport()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Method { get; set; }
        public double? Price { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
