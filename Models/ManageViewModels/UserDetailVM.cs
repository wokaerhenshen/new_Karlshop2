using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Models.ManageViewModels
{
    public class UserDetailVM
    {
        [Required]
        [Display(Name = "ID")]
        public string id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone")]
        public string phone { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string address { get; set; }
    }
}
