using new_Karlshop.Data;
using new_Karlshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Repository
{
    public class AccountGoodsRepo
    {
        ApplicationDbContext _context;
        public AccountGoodsRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<AccountGood> GetAll()
        {
            return _context.AccountGoods;
        }

        public IEnumerable<AccountGood> GetCartByAccountID(string id)
        {
            IEnumerable<AccountGood> ags = _context.AccountGoods.Where(a => a.Account_ID == id);
            return ags;
        }

        public AccountGood GetOneGoodByBothID(string accountID, int goodsID)
        {
            AccountGood ag = _context.AccountGoods.Where(a => a.Goods_ID == goodsID && a.Account_ID == accountID && a.Type == "cart").FirstOrDefault();
            return ag;
        }

        public int GenerateOrderId()
        {
            if (_context.AccountGoods.Count() == 0)
            {
                return 1;
            }
            else
            {
                return _context.AccountGoods.Select(o => o.Order_ID).Max()+1;
            }

        }

        public void DeleteOneGoodinWishByBothID(string accountID, int goodsID)
        {
            AccountGood ag = _context.AccountGoods.Where(a => a.Goods_ID == goodsID && a.Account_ID == accountID && a.Type == "wishlist").FirstOrDefault();
            _context.AccountGoods.Remove(ag);
            _context.SaveChanges();

        }


        public void DeleteOneGoodinCartByBothID(string accountID, int goodsID)
        {
            AccountGood ag = _context.AccountGoods.Where(a => a.Goods_ID == goodsID && a.Account_ID == accountID && a.Type == "cart").FirstOrDefault();
            _context.AccountGoods.Remove(ag);
            _context.SaveChanges();

        }

        public void UpdateOneGoodByBothID(AccountGood accountGood)
        {
            AccountGood ag = _context.AccountGoods.Where(a => a.Goods_ID == accountGood.Goods_ID && a.Account_ID ==accountGood.Account_ID && a.Type == "cart").FirstOrDefault();
            ag.Quantity = accountGood.Quantity;
            _context.SaveChanges();

        }

        public void WishToCart(string  AccountID,int id)
        {
            AccountGood ag = _context.AccountGoods.Where(a => a.Goods_ID == id && a.Account_ID == AccountID && a.Type == "wishlist").FirstOrDefault();
            if (_context.AccountGoods.Where(a => a.Goods_ID == id && a.Account_ID == AccountID && a.Type == "cart").FirstOrDefault()== null)
            {
                ag.Type = "cart";
            }
            else
            {
                _context.AccountGoods.Where(a => a.Goods_ID == id && a.Account_ID == AccountID && a.Type == "cart").FirstOrDefault().Quantity++;
            }

            _context.SaveChanges();
        }

        public void AddtoViewedItem(string accountID, int id)
        {
            AccountGood ag = _context.AccountGoods.Where(a => a.Account_ID == accountID && a.Goods_ID == id && a.Viewed == true).FirstOrDefault();
            if (ag == null)
            {
                AccountGood temp = new AccountGood()
                {
                    Order_ID = GenerateOrderId(),
                    Account_ID = accountID,
                    Goods_ID = id,
                    Viewed = true
                };
                _context.AccountGoods.Add(temp);
                _context.SaveChanges();
            }
            else
            {
                ag.Order_ID = GenerateOrderId();
                _context.SaveChanges();
            }
        }

        public List<Goods> recommendedGoods(string accountID)
        {
            CartRepo cartRepo = new CartRepo(_context);
            ViewedVM RecentViewed= cartRepo.GetViewedAll(accountID).ToList().FirstOrDefault();
            int category = RecentViewed.cat_id;
            return _context.Goodses.Where(cat => cat.cat_id == category).Select(All => All).Take(4).ToList();
        }

    }
}
