using System;
using System.Collections.Generic;

#nullable disable

namespace HockeyShop1.Models
{
    public partial class ShoppinCart
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
