using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TVLSecurity.Web.Models;
using System.Web.UI;
using Entity;
using System.Collections;
using PrimaryBaseLibrary;

namespace TVLSecurity.Web.Controllers
{
    [NoCache]
    public class RoleGroupController : Controller
    {
        //
        // GET: /RoleGroup/
        [NoCache]
        public ActionResult Index()
        {
            ViewData["PageTitle"] = "Role Group";
            List<RoleGroup> roleGroupList = UserRepository.GetRoleGroups();
            return View(roleGroupList);
        }

        //
        // GET: /RoleGroup/Details/5
        [NoCache]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /RoleGroup/Create
        [NoCache]
        public ActionResult Create()
        {

            return PartialView("Create", new RoleGroup());
        }

        //
        // POST: /RoleGroup/Create

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult Create(RoleGroup rgroup)
        {
            try
            {
                if (ValidateRoleGroupModel(rgroup))
                {
                    rgroup.ApplicationId = 1;
                    rgroup.RoleGroupsId = UserRepository.CreateRoleGroup(rgroup);
                    rgroup.IsValid = true;


                }
                return PartialView("Create", rgroup);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // GET: /RoleGroup/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {

            return View(UserRepository.GetRoleGroupById(id));
        }

        //
        // POST: /RoleGroup/Edit/5


        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult Edit(RoleGroup rgroup)
        {
            try
            {
                if (ValidateRoleGroupModel(rgroup))
                {                    
                    UserRepository.UpdateRoleGroup(rgroup);
                    rgroup.IsValid = true;
                }
                return PartialView("Edit", rgroup);
            }
            catch
            {
                return View("Error");
            }
        }

        [NoCache]
        private bool ValidateRoleGroupModel(RoleGroup model)
        {
            ModelState.Clear();

            if (String.IsNullOrEmpty(model.RoleGroupsTitle))
            {
                ModelState.AddModelError("model.RoleGroupsTitle", "The Role Group Title field required.");
            }



            return ModelState.IsValid;
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
