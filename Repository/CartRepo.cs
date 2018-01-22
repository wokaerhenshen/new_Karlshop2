using new_Karlshop.Data;
using new_Karlshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace new_Karlshop.Repository
{
    

    public class CartRepo
    {

        ApplicationDbContext db;

        public CartRepo(ApplicationDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<CartVM> GetCartAll(string id)
        {
            IEnumerable<CartVM> query = from ag in db.AccountGoods
                                        where (ag.Account_ID == id && ag.Type == "cart")
                                        select new CartVM
                                        {
                                            Goods_ID = ag.Goods_ID,
                                            Goods_Name = ag.Goods.goods_name,
                                            Account_ID = ag.Account_ID,
                                            Shop_Price = ag.Goods.shop_price,
                                            Quantity = ag.Quantity
                                        };
            return query;
        }

        public IEnumerable<BoughtVM> GetBoughtAll(string id)
        {
            IEnumerable<BoughtVM> query = from ag in db.AccountGoods
                                        where (ag.Account_ID == id && ag.Type == "bought")
                                        select new BoughtVM
                                        { 
                                            Account_ID = ag.Account_ID,
                                            Good_ID = ag.Goods_ID,
                                            Order_ID = ag.Order_ID,
                                            Goods_Name = ag.Goods.goods_name,
                                            Shop_Price = ag.Goods.shop_price,
                                            Quantity = ag.Quantity
                                        };
            return query;
        }



    }

}
