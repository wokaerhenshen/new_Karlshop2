using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace new_Karlshop.Models.AccountViewModels
{
    public class UserRoleVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }

    }
}
