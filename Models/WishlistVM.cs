using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Models
{
    public class WishlistVM
    {
        public string AccountID { get; set; }
        public int goodsID { get; set; }


        [Display(Name = "Good Name")]
        public string goodsName { get; set; }

        [Display(Name = "Good Description")]
        public string goodsDescript { get; set; }

        [Display(Name ="Price")]
        public decimal goodsPrice { get; set; }

    }
}
