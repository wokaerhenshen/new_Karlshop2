using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Models
{
    public class CartVM
    {
        public string Account_ID { get; set; }
        public int Goods_ID { get; set; }
        public string Goods_Name { get; set; }
        public decimal Shop_Price { get; set; }
        public int Quantity { get; set; }
        public string GoodImg { get; set; }
    }
}
