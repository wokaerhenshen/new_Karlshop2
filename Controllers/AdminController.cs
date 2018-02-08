using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using new_Karlshop.Data;
using new_Karlshop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace new_Karlshop.Controllers
{
    //How can I do the validation if input is not model?
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ApplicationDbContext _context;
        CategoryRepo cr;
        GoodsRepo gr;
        AccountRepo ar;
        AccountGoodsRepo ag;
        OrderRepo og;

        public AdminController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context,
            IHostingEnvironment hostingEnvironment)
        {
            this._httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
            this._context = context;
            this.cr = new CategoryRepo(context);
            this.gr = new GoodsRepo(context);
            this.ar = new AccountRepo(context);
            this.ag = new AccountGoodsRepo(context);
            this.og = new OrderRepo(context);
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin
        public ActionResult Account()
        {
            ViewBag.menuActive = "Admin";
            return View(ar.GetAll());
        }

        // GET: Admin
        public ActionResult Category()
        {
            ViewBag.menuActive = "Admin";
            return View(cr.GetAll());
        }

        // GET: Admin
        public ActionResult Goods()
        {
            ViewBag.menuActive = "Admin";
            return View(gr.getAll());
        }

        // GET: Admin
        public ActionResult AccountEdit(string id)
        {
            ViewBag.menuActive = "Admin";
            return View(ar.GetOneAccountByNum(id));
        }

        // POST: Account Edit update
        [HttpPost]
        public ActionResult AccountEdit(Account account)
        {
            if(ModelState.IsValid)
            ar.EditOneAccount(account);
            return RedirectToAction("Account", "Admin");
        }

        // GET: Admin
        public ActionResult AccountDelete(string id)
        {
            ar.DelOneAccountByNum(id);
            return RedirectToAction("Account", "Admin");
        }

        // GET: Admin
        public ActionResult AccountCreate()
        {
            return View(ar.GetOneAccountByNum("0"));
        }

        // POST: Admin Save Create
        [HttpPost]
        public ActionResult AccountCreate(Account account)
        {
            account.Id = ar.GetAccountMaxID() + 1;

            ar.AddOneAccount(account);


            return RedirectToAction("Account", "Admin");
        }

        // GET: Admin
        public ActionResult CategoryEdit(int id)
        {

            ViewBag.menuActive = "Admin";
            return View(cr.GetOneCategoryByID(id));
        }

        // POST: Cart Edit update
        [HttpPost]
        public ActionResult CategoryEdit(Category category)
        {
            cr.EditOneCategory(category);

            return RedirectToAction("Category", "Admin");
        }


        // GET: Admin
        public ActionResult CategoryDelete(int id)
        {
            cr.DelOneCategoryByID(id);

            return RedirectToAction("Category", "Admin");
        }

        // GET: Admin
        public ActionResult CategoryCreate()
        {
            ViewBag.menuActive = "Admin";
            ViewBag.MyCatSelectList = cr.GetPCatSelectList();

            return View(cr.GetOneCategoryByID(1));
        }

        // POST: Admin Save Create
        [HttpPost]
        public ActionResult CategoryCreate(Category category)
        {
            category.cat_id = cr.GetCategoryMaxID(category.parent_id) + 1;

            cr.AddOneCategory(category);

            return RedirectToAction("Category", "Admin");
        }

        // GET: Admin
        public ActionResult GoodsEdit(int id)
        {

            ViewBag.menuActive = "Admin";
            return View(gr.GetOneGoods(id));
        }

        [HttpPost]
        public ActionResult GoodsEdit(Goods goods)
        {
            gr.EditOneGoods(goods);

            return RedirectToAction("Goods", "Admin");
        }


        // GET: Admin
        public ActionResult GoodsDelete(int id)
        {
            gr.DeletOneGoods(id);

            return RedirectToAction("Goods", "Admin");
        }

        // GET: Admin
        public ActionResult GoodsCreate()
        {
            GetAspCookie();
            ViewBag.menuActive = "Admin";
            // FOR SELECT Goods Category
            ViewBag.MySubCatSelectList = cr.GetSubCatSelectList();

            return View(gr.GetOneGoods(0));
        }

        // POST: Admin Save Create
        [HttpPost]
        public ActionResult GoodsCreate(Goods goods)
        {
            goods.goods_id = gr.GetGoodsMaxID() + 1;

            goods.last_update = DateTime.Now;

            gr.AddOneGoods(goods);

            return RedirectToAction("Goods", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> FileSave()
        {
            var date = Request;
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {

                    //I will ask abou this :
                    //感谢分享，请问在第二种方法 ajax上传中，为何要随机生成一个新的文件名呢，用原来的不好吗？
                    string fileExt = Path.GetExtension(formFile.FileName); //文件扩展名，不含“.”
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位
                    string fileName = formFile.FileName;
                    string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名
                    var filePath = webRootPath + "\\images\\" + fileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {

                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            //why not it return this msg?
            return Ok(new { message = "upload success!!!" });
        }

        [HttpGet]
        public ActionResult CustomerOrders()
        {
            
            return View(og.GetAll());
        }

        //So I have two options, the first one is to add stuff in database, the second one is to create viewmodel.
        [HttpGet]
        public ActionResult OrderDetails(int id)
        {
            return View(og.GetOrderVM_InOneOrder(id));
        }

        public ActionResult Transactions()
        {
            return View(og.GetAllTransaction());
        }




        private string GetAspCookie()
        {
            CookieHelper cookieHelper = new CookieHelper(_httpContextAccessor, Request,
                                                 Response);

            ViewBag.loginUser = cookieHelper.Get(CookieHelper.USER_NAME);

            return (ViewBag.loginUser);
        }
    }
}