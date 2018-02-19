using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using new_Karlshop.Data;
using new_Karlshop.Repository;
using Microsoft.AspNetCore.Http;
using new_Karlshop.Services;

namespace new_Karlshop.Controllers
{
    public class BaseController : Controller
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.someThing = "someThing"; //Add whatever
            //ViewBag.ViewedItems = CartRepo.GetViewedAll(User.getUserId(),).ToList();
            base.OnActionExecuting(filterContext);
        }
    }
}