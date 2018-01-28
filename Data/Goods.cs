using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Data
{
    //define what a good should have
    public class Goods
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Goods_ID")]
        public int goods_id { get; set; }

        [Display(Name = "Cat_ID")]
        public int cat_id { get; set; }

        [Display(Name = "Goods_SN")]
        public string goods_sn { get; set; }

        [Display(Name = "Goods Name")]
        public string goods_name { get; set; }

        [Display(Name = "Shop Price")]
        public decimal shop_price { get; set; }

        [Display(Name = "Market Price")]
        public decimal market_price { get; set; }

        [Display(Name = "Quantity")]
        public int goods_quantity { get; set; }

        [Display(Name = "Sold Quantity")]
        public int sold_quantity { get; set; }

        [Display(Name = "Weight")]
        public decimal goods_weight { get; set; }

        [Display(Name = "Goods Description")]
        public string goods_desc { get; set; }

        [Display(Name = "Goods Biref Description")]
        public string goods_brief { get; set; }

        [Display(Name = "Image Path")]
        public string ori_img { get; set; }

        [Display(Name = "Image Path")]
        public string ori_img1 { get; set; }

        [Display(Name = "Image Path")]
        public string ori_img2 { get; set; }

        [Display(Name = "Deleted")]
        public Boolean is_delete { get; set; }

        [Display(Name = "Post Free")]
        public Boolean is_free_post { get; set; }

        [Display(Name = "Updated Time")]
        public DateTime last_update { get; set; }

        //public string comment_1 { get; set; }
        
        //define the parent of goods.
        public virtual Category Category { get; set; }

        public virtual ICollection<AccountGood> AccountGood { get; set; }
    }
}
