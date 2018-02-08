using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Models
{
    public class OrderDetailVM
    {
        [Display(Name = "Good Name")]
        public string goodsName { get; set; }

        [Display(Name = "Quantity")]
        public int quantity { get; set; }
        
    }
}
