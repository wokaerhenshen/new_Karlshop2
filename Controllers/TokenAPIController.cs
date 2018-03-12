using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using new_Karlshop.Models;
using Microsoft.Extensions.Configuration;
using new_Karlshop.Models.AccountViewModels;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using new_Karlshop.Repository;
using new_Karlshop.Data;
using new_Karlshop.Services;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace new_Karlshop.Controllers
{
    //[Produces("application/json")]
    // [Route("api/TokenAPI")]
    //public class TokenAPIController : Controller
    //{
    //}
    [Produces("application/json")]
    [Route("api/TokenAPI")]
    [Route("[controller]/[action]")]
    public class TokenAPI : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public ApplicationDbContext _context;
        GoodsRepo gr;
        CategoryRepo cr;

        // Constructor.
        public TokenAPI(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                IConfiguration configuration,
                ApplicationDbContext context,
                IHostingEnvironment hostingEnvironment
            )
        {
            this._context = context;
            _hostingEnvironment = hostingEnvironment;
            this.gr = new GoodsRepo(context);
            cr = new CategoryRepo(context);
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public List<string> GetUsers()
        {
            List<string> userEmail = new List<string>();
            userEmail = _context.Users.Select(e => e.Email).ToList();

            return userEmail;
        }

        // Generates a fake collection for demonstration purposes. 
        public List<LoginViewModel> GetFakeData()
        {
            List<LoginViewModel> logins = new List<LoginViewModel>();
            logins.Add(new LoginViewModel()
            {
                Email = "bob@home.com",
                Password = "password"
            });
            logins.Add(new LoginViewModel()
            {
                Email = "fakeuser@home.com",
                Password = "password"
            });
            return logins;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize]
        public IEnumerable<WishlistVM> Wishlist()
        {
            CartRepo cart = new CartRepo(_context);
            return cart.GetWishAll(User.getUserId());
        }

        [HttpPost]
        public IEnumerable<Goods> SellingList([FromBody]ApplicationUser ApplicationUser)
        {
            //GoodsRepo good = new GoodsRepo(_context);
            return _context.Goodses.Where(sl => sl.seller == ApplicationUser.Email);
        }

        // This Action method does not require authentication.
        [HttpGet]
        public IEnumerable<Goods> Public()
        {
            return gr.getAll();
        }

        [HttpGet]
        public List<SelectListItem> GetCategory()
        {
            return cr.GetSubCatSelectList();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public string AddGood([FromBody] Goods good)
        {
            //good.goods_name
            good.goods_id = gr.GetGoodsMaxID() + 1;
            good.last_update = DateTime.Now;
            gr.AddOneGoods(good);
            _context.SaveChanges();
            return good.goods_name;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public bool EditGood([FromBody] Goods good)
        {
            Goods updatingGood = _context.Goodses.Where(c => c.goods_id == good.goods_id).FirstOrDefault();
            updatingGood.goods_name = good.goods_name;
            updatingGood.shop_price = good.shop_price;
            updatingGood.market_price = good.market_price;
            updatingGood.goods_quantity = good.goods_quantity;
            updatingGood.goods_weight = good.goods_weight;
            updatingGood.goods_desc = good.goods_desc;
            updatingGood.goods_brief = good.goods_brief;
            updatingGood.is_free_post = good.is_free_post;
            _context.SaveChanges();
            return true;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public bool DeleteGood([FromBody] Goods good)
        {
            Goods deletingGood = _context.Goodses.Where(c => c.goods_name == good.goods_name && c.seller == good.seller).FirstOrDefault();
            _context.Goodses.Remove(deletingGood);
            _context.SaveChanges();
            return true;
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            var files = Request.Form.Files;
            string webRootPath = _hostingEnvironment.WebRootPath;
            List<string> filenames = new List<string>();

            foreach (var file in files)
            {
                // to do save
                //I will ask abou this :
                //感谢分享，请问在第二种方法 ajax上传中，为何要随机生成一个新的文件名呢，用原来的不好吗？
                string fileExt = Path.GetExtension(file.FileName); //文件扩展名，不含“.”
                long fileSize = file.Length; //获得文件大小，以字节为单位
                string fileName = file.FileName;
                filenames.Add(fileName);
                string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名
                var filePath = webRootPath + "\\images\\" + fileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

            }

            return Ok(filenames);
        }

        // This Action method requires authentication.
        [HttpGet]
        // Since we have cookie authentication and Jwt authentication we must
        // specify that we want Jwt authentication here.
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IEnumerable<LoginViewModel> Protected()
        {
            return GetFakeData();
        }

        // This Action method enables web api login but with the Identity framework
        // that was added when the project was generated by the wizard.
        [HttpPost]
        public async Task<object> Login([FromBody] LoginVM model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                                model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r =>
                                            r.Email == model.Email);
                return GenerateJwtToken(model.Email, appUser);
            }
            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        // This Action method lets us register new users with the Identity framework.
        [HttpPost]
        public async Task<object> Register([FromBody] RegisterVM model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return GenerateJwtToken(model.Email, user);
            }
            throw new ApplicationException("UNKNOWN_ERROR");
        }

        // Generates a token using settings stored in the appsettings.json file.
        private object GenerateJwtToken(string email,
                                                    IdentityUser user)
        {
            var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            _configuration["TokenInformation:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(Convert.ToDouble(1));
            var token = new JwtSecurityToken(
                _configuration["TokenInformation:Issuer"],
                _configuration["TokenInformation:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            var formattedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = formattedToken });
        }

        // It would be better to put this class in a ViewModels folder.
        public class LoginVM
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        // It would be better to put this class in a ViewModels folder.
        public class RegisterVM
        {
            [Required]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
            public string Password { get; set; }
        }
    }



}