using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeyShop1
{
    class SortedProductQuery
    {
        public int PropID { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public double? Price { get; set; }
        public string Color { get; set; }
    }
}
