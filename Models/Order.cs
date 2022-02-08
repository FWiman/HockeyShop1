using System;
using System.Collections.Generic;

#nullable disable

namespace HockeyShop1.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? TransportId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Transport Transport { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
