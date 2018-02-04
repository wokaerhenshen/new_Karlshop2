using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Data
{
    public class IPN
    {
        [Key]
        [Display(Name = "ID")]
        public string transactionID { get; set; }
        [Display(Name = "Transaction Time")]
        public string txTime { get; set; }

        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Display(Name = "Buyer Email")]
        public string buyerEmail { get; set; }
        [Display(Name = "Amount")]
        public string amount { get; set; }
        [Display(Name = "Payment Status")]
        public string paymentStatus { get; set; }

    }
}
