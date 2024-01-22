using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TVLSecurity.Web.Models;
using Entity;
using PrimaryBaseLibrary;

namespace TVLSecurity.Web.Controllers
{
    [HandleError]
    [NoCache]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["PageTitle"] = "Dashboard";
            DashboardViewModel model = new DashboardViewModel();          
            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public JsonResult Dashboard ()
        {
            DashboardViewModel model = new DashboardViewModel();
            model.GetZoneWiseUserList();
            return Json(model,JsonRequestBehavior.AllowGet);

        }
    }
}
