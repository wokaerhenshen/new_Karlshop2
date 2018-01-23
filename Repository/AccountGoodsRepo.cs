using new_Karlshop.Data;
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


        public void DeleteOneGoodByBothID(string accountID, int goodsID)
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


    }
}
