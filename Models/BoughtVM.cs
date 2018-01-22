using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Models
{
    public class BoughtVM
    {
        public string Account_ID { get; set; }
        public int Good_ID { get; set; }
        public int Order_ID { get; set; }
        public string Goods_Name { get; set; }
        public decimal Shop_Price { get; set; }
        public int Quantity { get; set; }
    }
}
