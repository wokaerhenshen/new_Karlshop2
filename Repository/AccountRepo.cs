using new_Karlshop.Data;
using new_Karlshop.Models.ManageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Repository
{
    public class AccountRepo
    {
        ApplicationDbContext _context;

        public AccountRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Accounts;
        }

        public int generateNewOrderID()
        {
            if (_context.OrderGoods.Count() == 0)
            {
                return 1;
            }
            else
            {
                return _context.OrderGoods.Select(o => o.Order_id).Max() + 1;
            }
        }

        public Boolean FindAccount(string username)
        {
            Account exist = new Account();
            exist =  _context.Accounts.Where(un => un.ApplicationUser.UserName == username).FirstOrDefault();
            if (exist != null)
            {
                return true;
            }
            return false;
        }

        public string GetAccountMaxID()
        {
            return _context.Accounts.Select(id => id.Id).Max(); 
        }


        //public int GetAccountNumByUserName(string username)
        //{
        //    return _context.Accounts.Where(un => un.ApplicationUser.UserName == username).Select(num => num.Id).FirstOrDefault();
        //}

        public void DelOneAccountByNum(string id)
        {
            Account account = _context.Accounts.Where(a => a.Id == id).FirstOrDefault();
            IEnumerable<AccountGood> accountGoods = _context.AccountGoods.Where(a => a.Account_ID == id);
            foreach (var accountGood in accountGoods)
            {
                _context.AccountGoods.Remove(accountGood);
            }
            _context.SaveChanges();
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }


        public Account GetOneAccountByNum(string id)
        {
            return _context.Accounts.Where(a => a.Id == id).FirstOrDefault();
        }

        public IEnumerable<UserDetailVM> getOneUserDetailByNum(string id)
        {
            IEnumerable<UserDetailVM> query = from ac in _context.Accounts
                                 where (ac.Id == id)
                                 select new UserDetailVM
                                 {
                                     id = ac.Id,
                                     firstName = ac.firstName,
                                     lastName = ac.lastName,
                                     phone = ac.phone,
                                     address = ac.address
                                 };
            return query;
        }

        public void AddOneAccount(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public void EditOneAccount(Account account)
        {
           Account result = _context.Accounts.Where(a => a.Id == account.Id).FirstOrDefault();

           result.firstName = account.firstName;
           result.lastName = account.lastName;
          
           result.phone = account.phone;
           result.address  = account.address ;
            _context.SaveChanges();
        }

        public void QuickEditAccount(Account account)
        {
            Account result = _context.Accounts.Where(a => a.Id == account.Id).FirstOrDefault();
            result.firstName = account.firstName;
            result.lastName = account.lastName;
      
            result.phone = account.phone;
            result.address = account.address;
            _context.SaveChanges();
        }

        public void QuickEditAccountFromUserDetail(UserDetailVM userDetail)
        {
            Account account = _context.Accounts.Where(a => a.Id == userDetail.id).FirstOrDefault();
            account.firstName = userDetail.firstName;
            account.lastName = userDetail.lastName;
            account.phone = userDetail.phone;
            account.address = userDetail.address;
            _context.SaveChanges();
        }

    }
}

