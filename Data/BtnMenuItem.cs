using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Data
{
  
        public class MenuItem
        {
            public string menuName { get; set; }
            public int menuPath { get; set; }

            public virtual BtnMenuItem BtnMenuItem { get; set; }
        }

        public class BtnMenuItem
        {
            public MenuItem mainMenu { get; set; }
            //public List<MenuItem> dropMenu { get; set; }

            public virtual ICollection<MenuItem> MenuItem { get; set; }
        }
    
}
