using new_Karlshop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Repository
{
    
    // I need to change something when making the viewmodel.
    public class OrderRepo
    {
        ApplicationDbContext _context;
        public OrderRepo( ApplicationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders;
        }

        public IEnumerable<OrderGoods> GetGoodsInOneOrder(int id) {
            return _context.OrderGoods.Where(og => og.Order_id == id);
        }


    }
}
