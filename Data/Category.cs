using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Data
{
    public class Category
    {
        //define what a category should have
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Cat_ID")]
        public int cat_id { get; set; }

        [Display(Name = "Parent_ID")]
        public int parent_id { get; set; }

        [Display(Name = "Category Name")]
        public string cat_name { get; set; }

        [Display(Name = "Description")]
        public string intro { get; set; }

        public virtual ICollection<Goods> Goods { get; set; }


    }




}
