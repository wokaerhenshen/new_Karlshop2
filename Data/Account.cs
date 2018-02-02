using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using new_Karlshop.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Data

{
    public class Account
    {

        [Key]
        public string Id { get; set; }
 
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Display(Name = "Phone")]
        public string phone { get; set; }

        [Display(Name = "Address")]
        public string address { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<AccountGood> AccountGood { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
