using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TVLSecurity.Web.Models;
using System.Web.UI;
using Entity;
using PrimaryBaseLibrary;


namespace TVLSecurity.Web.Controllers
{
    [NoCache]
    public class TreeController : Controller
    {
        //
        // GET: /Tree/
        [NoCache]
        public ActionResult Index()
        {
            return View();
        }

       

    }
}
