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

        public IEnumerable<WishlistVM> GetWishAll(string id)
        {
            IEnumerable<WishlistVM>  query = from ag in db.AccountGoods
                    where (ag.Account_ID == id && ag.Type == "wishlist")
                    select new WishlistVM
                    {
                        AccountID = ag.Account_ID,
                        goodsID = ag.Goods_ID,
                        goodsName = ag.Goods.goods_name,
                        goodsDescript = ag.Goods.goods_brief,
                        goodsPrice = ag.Goods.shop_price
                    };
            return query;
        }

        public IEnumerable<ViewedVM> GetViewedAll(string id)
        {
            IEnumerable<ViewedVM> query = (from ag in db.ViewedGoods
                                           where (ag.Account_ID == id)
                                           orderby ag.ViewedSequence descending
                                           select new ViewedVM
                                           {
                                               viewedSequence = ag.ViewedSequence,
                                               goodName = ag.Goods.goods_name,
                                               goodBreif = ag.Goods.goods_brief,
                                               goodImg = ag.Goods.ori_img,
                                               cat_id = ag.Goods.cat_id

                                           }).Distinct();
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
