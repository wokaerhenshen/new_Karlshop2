using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using new_Karlshop.Models;
using new_Karlshop.Data;
using Microsoft.AspNetCore.Http;
using new_Karlshop.Repository;
using Microsoft.AspNetCore.Authorization;
using new_Karlshop.Services;

namespace new_Karlshop.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext _context;
        CategoryRepo cr;
        GoodsRepo gr;
        AccountRepo ar;
        AccountGoodsRepo ag;

        public HomeController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._context = context;
            this.cr = new CategoryRepo(context);
            this.gr = new GoodsRepo(context);
            this.ar = new AccountRepo(context);
            this.ag = new AccountGoodsRepo(context);
        }

        public IActionResult Welcome()
        {
            ViewBag.all_img_phone = gr.GetAllData_Phone();
            ViewBag.all_data_laptop = gr.GetAllData_Laptop();
            ViewBag.all_data_tv = gr.GetAllData_Tv();
            ViewBag.popular_items = gr.GetPopularItem();

            return View();

        }


        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {


            ViewBag.MyViewBagList = cr.GetMenuList();
            ViewBag.menuActive = "Home";


            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.AbsolutePath = Request.Path;

            IEnumerable<Goods> goods = gr.GetGoods(searchString, sortOrder);

            // Store current search and sort filter parameters.
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentSort = sortOrder;

            // Provide toggle option for name sort.
            if (String.IsNullOrEmpty(sortOrder))
                ViewBag.NameSortParm = GoodsRepo.NAME;
            else
                ViewBag.NameSortParm = "";

            // Provide toggle  optionfor date sort.
            if (sortOrder == GoodsRepo.DATE)
                ViewBag.DateSortParm = GoodsRepo.DATE_DESC;
            else
                ViewBag.DateSortParm = GoodsRepo.DATE;

            //const int PAGE_SIZE = 6;
            int pageNumber = (page ?? 1);

            // Truncate results to fit in the view provided.
            // goods = goods.ToPagedList(pageNumber, PAGE_SIZE);

            return View(goods);
        }

        public ActionResult SortIndex(int id, string sortOrder)
        {

            ViewBag.MyViewBagList = cr.GetMenuList();

            // Store current sort filter parameter.
            ViewBag.CurrentSort = sortOrder;

            ViewBag.menuActive = "Home";

            switch (sortOrder)
            {
                case GoodsRepo.NAME:
                    ViewBag.NameSortParm = GoodsRepo.NAME_DESC;
                    ViewBag.DateSortParm = GoodsRepo.DATE;
                    ViewBag.IdSortParm = GoodsRepo.ID;
                    break;
                case GoodsRepo.NAME_DESC:
                    ViewBag.NameSortParm = GoodsRepo.NAME;
                    ViewBag.DateSortParm = GoodsRepo.DATE;
                    ViewBag.IdSortParm = GoodsRepo.ID;
                    break;
                case GoodsRepo.ID:
                    ViewBag.IdSortParm = GoodsRepo.ID_DESC;
                    ViewBag.NameSortParm = GoodsRepo.NAME;
                    ViewBag.DateSortParm = GoodsRepo.DATE;
                    break;
                case GoodsRepo.ID_DESC:
                    ViewBag.IdSortParm = GoodsRepo.ID;
                    ViewBag.NameSortParm = GoodsRepo.NAME;
                    ViewBag.DateSortParm = GoodsRepo.DATE;
                    break;
                case GoodsRepo.DATE:
                    ViewBag.DateSortParm = GoodsRepo.DATE_DESC;
                    ViewBag.IdSortParm = GoodsRepo.ID;
                    ViewBag.NameSortParm = GoodsRepo.NAME;
                    break;
                case GoodsRepo.DATE_DESC:
                    ViewBag.DateSortParm = GoodsRepo.DATE;
                    ViewBag.IdSortParm = GoodsRepo.ID;
                    ViewBag.NameSortParm = GoodsRepo.NAME;
                    break;
                default:
                    ViewBag.DateSortParm = GoodsRepo.DATE;
                    ViewBag.IdSortParm = GoodsRepo.ID;
                    ViewBag.NameSortParm = GoodsRepo.NAME;
                    break;
            }

            if (id == 0)
            {   // ALL Goods, By sortOrder
                return View(GoodsRepo.SortGoods(gr.getAll(), sortOrder));
            }

            if (id < 10)
            {
                // Get Goods  From Parent ID 
                var query = from n in gr.getAll()
                            let tmp = cr.GetAll().Where(x => x.parent_id == id).Select(x => x.cat_id)
                            where tmp.Contains(n.cat_id)
                            select n;

                return View(GoodsRepo.SortGoods(query, sortOrder));
            }

            // Get goods by cat_id and sortOrder
            return View(GoodsRepo.SortGoods(GoodsRepo.GetGoodsByCatID(gr.getAll(), id), sortOrder));
        }

        public ActionResult ShowGoods(int id)
        {
            ViewBag.MyViewBagList = cr.GetMenuList();
            ViewBag.menuActive = "Gallery";

            ViewBag.imgPath = "";

            // I think I need to firgure out how to do the delete operation in the future.
            //foreach ( var good in gr.getAll().ToList())
            //{
            //    if (good.goods_quantity == 0)
            //    {
            //        _context.Goodses.Remove(good);
            //        _context.SaveChanges();
            //    }
            //}



            if (id == 0)
            {   // ALL button
                ViewBag.goods = gr.getAll().ToList();
                return View(gr.getAll());

            }

            if (id < 10)
            {
                // Get Goods  From Parent ID 
                var query = from n in gr.getAll()
                            let tmp = cr.GetAll().Where(x => x.parent_id == id).Select(x => x.cat_id)
                            where tmp.Contains(n.cat_id)
                            select n;

                ViewBag.goods = query.ToList();

                return View(query);
            }

            ViewBag.goods = GoodsRepo.GetGoodsByCatID(gr.getAll(), id);

            return View(GoodsRepo.GetGoodsByCatID(gr.getAll(), id));
        }


        public ActionResult GoodsEdit(int id)
        {

            ViewBag.menuActive = "Gallery";

            ViewBag.imgPath = "/images/";

            ViewBag.myComments = gr.GetCommentsByGoodID(id).ToList();
            ViewBag.commentExist = gr.GetCommentsByGoodID(id).ToList().FirstOrDefault();
            ViewBag.maxGoodID = gr.GetMaxID();
            return View(gr.GetOneGoods(gr.getAll(), id));
        }

        [Authorize]
        public ActionResult ShowCart(int id)
        {
        
            List<AccountGood> accountGoods = _context.AccountGoods.Where(ag => ag.Account_ID == _context.Users.Where(name => name.UserName == User.Identity.Name).Select(i => i.Id).FirstOrDefault()).ToList();
            ViewBag.menuActive = "Cart";

            if (id != 0)
            {
                
                AccountGood item = accountGoods.Where(ag => ag.Goods_ID == id && ag.Type == "cart").FirstOrDefault();
                if (item != null)
                {
                    item.Quantity++;
                    _context.SaveChanges();
                }

                else
                {
                    AccountGood temp = new AccountGood()
                    {
                        Order_ID = ag.GenerateOrderId(),
                        Account_ID = _context.Users.Where(name => name.UserName == User.Identity.Name).Select(i => i.Id).FirstOrDefault(),
                        Goods_ID = id,
                        Quantity = 1,
                        Type = "cart"

                    };
                    _context.AccountGoods.Add(temp);
                    _context.SaveChanges();
                }

            }

            //why I have to define this again?
            List<AccountGood> NewaccountGoods = _context.AccountGoods.Where(ag => ag.Account_ID == User.getUserId() && ag.Type == "cart").ToList();

            var totalPieces = 0m;
            var totalPrice = 0m;
            foreach (var item in NewaccountGoods)
            {
                totalPieces += item.Quantity;
                // item.subCount = item.shop_price * item.quantity;
                totalPrice += gr.GetOneGoods(item.Goods_ID).shop_price * item.Quantity;
            }

            ViewBag.totalPieces = totalPieces;
            ViewBag.totalPrice = totalPrice;

            // ViewBag.test = _context.AccountGoods.Where(ag => ag.Account_ID == Int32.Parse(cookieHelper.GetValue("KarlShop"))).FirstOrDefault().Account.address;
            //   return View(ag.GetCartByAccountID(Int32.Parse(cookieHelper.GetValue("KarlShop"))));

            CartRepo cart = new CartRepo(_context);

            return View(cart.GetCartAll(User.getUserId()));

        }


        [Authorize]
        public IActionResult ConfirmOrder(string accountID)
        {
            Account account = ar.GetOneAccountByNum(accountID);
            return View(account);
        }

        [HttpPost]
        public IActionResult ConfirmOrder(Account account)
        {
            CookieHelper cookieHelper = new CookieHelper(_httpContextAccessor, Request,
                         Response);

            ar.QuickEditAccount(account);
            List<AccountGood> CartaccountGoods = _context.AccountGoods.Where(ag => ag.Account_ID == _context.Users.Where(name => name.UserName == User.Identity.Name).Select(i => i.Id).FirstOrDefault() && ag.Type == "cart").ToList();


            
            foreach (var item in CartaccountGoods)
            {
                item.Type = "bought";
                _context.SaveChanges();
                Goods good = _context.Goodses.Where(i => i.goods_id == item.Goods_ID).FirstOrDefault();
                good.goods_quantity -= item.Quantity;
                good.sold_quantity += item.Quantity;
                _context.SaveChanges();
            }

            return RedirectToAction("FinishShopping", "Home");
        }

        public IActionResult FinishShopping()
        {
            return View();
        }

        [Authorize]
        public IActionResult Bought()
        {

            //The code below is the old method that I use cookie to judge which data I should get in
            //the database.
            //CookieHelper cookieHelper = new CookieHelper(_httpContextAccessor, Request,
            //             Response);

            //if (cookieHelper.Get("KarlShop") == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            ViewBag.menuActive = "Bought";

            List<AccountGood> NewaccountGoods = _context.AccountGoods.
                                                Where(ag => ag.Account_ID == _context.Users.Where(name => name.UserName == User.Identity.Name).Select(i => i.Id).FirstOrDefault() && ag.Type == "bought").ToList();

            CartRepo cart = new CartRepo(_context);

            return View(cart.GetBoughtAll(_context.Users.Where(name => name.UserName == User.Identity.Name).Select(i => i.Id).FirstOrDefault()));
        }

        [HttpGet]
        public IActionResult WriteReview(string AccountID, int GoodID, int OrderID)
        {
            ViewBag.AccountID = AccountID;
            ViewBag.GoodID = GoodID;
            ViewBag.OrderID = OrderID;
            return View();

        }

        [HttpPost]
        public void WriteReview(string AccountID, int GoodID, int OrderID, string comments, long millidate, double star)
        {
            // I will do some changes here.
            DateTime date = HelperMethods.ConvertTime(millidate);
            gr.CreateOneComment(AccountID, GoodID, OrderID, comments, date, star);

            //return RedirectToAction("Contact", "Home");
        }

        public ActionResult EditCart(string accountID, int goodID)
        {

            ViewBag.menuActive = "Cart";
            return View(ag.GetOneGoodByBothID(accountID, goodID));

        }

        [HttpPost]
        public ActionResult EditCart(AccountGood accountGood)
        {
            ag.UpdateOneGoodByBothID(accountGood);

            return RedirectToAction("ShowCart", "Home");
        }

        public ActionResult DeleteCart(string accountID, int goodID)
        {

            ag.DeleteOneGoodByBothID(accountID, goodID);

            return RedirectToAction("ShowCart", "Home");
        }

        // POST: Search, and list search result 
        [HttpPost]
        public ActionResult Search(string search)
        {
            //return View(GoodsRepo.SearchGoodsByStr(search));
            return View(GoodsRepo.FilterGoods(gr.getAll(), search));
        }

        private string GetAspCookie()
        {
            CookieHelper cookieHelper = new CookieHelper(_httpContextAccessor, Request,
                                                 Response);

            ViewBag.loginUser = cookieHelper.Get(CookieHelper.USER_NAME);

            return (ViewBag.loginUser);
        }



        public IActionResult Contact()
        {
            ViewData["Message"] = "Welcome to Contact Me!";
            ViewBag.menuActive = "About";
            return View();
        }

        [HttpPost]
        public IActionResult EmailSender()
        {
            string full_name = Request.Form["fullname"];
            string emailaddress = Request.Form["emailaddress"];
            string phonenumber = Request.Form["phonenumber"];
            string message = Request.Form["message"];
            EmailHelper.send(full_name, emailaddress, phonenumber, message);
            return RedirectToAction("ShowCart", "Home");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
