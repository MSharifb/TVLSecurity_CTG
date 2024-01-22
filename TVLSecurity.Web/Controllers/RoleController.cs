using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TVLSecurity.Web.Models;
using System.Web.UI;
using Entity;
using System.Collections;
using TVLSecurity.Web.Code;
using PrimaryBaseLibrary;

namespace TVLSecurity.Web.Controllers
{
    [NoCache]
    public class RoleController : Controller
    {
        //
        // GET: /Role/
        [NoCache]
        public ActionResult Index()
        {
            ViewData["PageTitle"] = "Role";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [NoCache]
        public ActionResult List(int? page)
        {
            RoleFormViewModel model = new RoleFormViewModel(true);
            ModelState.Clear();
            model.CurrentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.PopulateRoleList();           

            return PartialView("List", model);
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //[NoCache]
        //public ActionResult List(int? page,RoleFormViewModel model)
        //{
        //    try
        //    {
        //        ModelState.Clear();
        //        model.CurrentPageIndex = page.HasValue ? page.Value - 1 : 0;
        //        model.PopulateRoleList();
        //        //model.GroupList = GetDropdownlistItems(model.SearchCraiteria.RoleGroupsId);
        //        return PartialView("List", model);
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error");
        //    }
        //}

        //
      
        //
        // GET: /Role/Create
        [NoCache]
        public ActionResult Create()
        {
            RoleFormViewModel model = new RoleFormViewModel(true);

            model.PopulateAppModSelectedList();

            model.Role.ApplicationId = model.ApplicationId  = -1;
            model.Role.ModuleId = model.ApplicationId =  -1;

            model.PopulateSelectedMenuList();

            model.PopulateRightList();
            
            //model.GroupList = GetDropdownlistItems(0);
            return PartialView("Create", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult Create(RoleFormViewModel model)
        {
            try
            {
                //model.MenuList = this.GetMenusWithData();

                if (ValidateRoleModel(model))
                {

                    model.CreateRole();

                    model.Role = new Role();
                    ModelState.Clear();
                }
                else
                {
                    throw new Exception();
                    
                }
            }
            catch
            {
                model.IsValidModel = false;
                if (model.MenuList == null)
                {
                    model.MenuList = new List<Menu>();
                }
                model.ApplicationId = model.Role.ApplicationId;
                model.ModuleId = model.Role.ModuleId;
                model.PopulateAppModSelectedList();
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
            }

            return PartialView("Create", model);
        }

        
        [NoCache]
        public ActionResult Edit(int id)
        {
            RoleFormViewModel model = new RoleFormViewModel(id);
           
          
            //model.GroupList = GetDropdownlistItems(model.Role.RoleGroupsId);
            return PartialView("Edit",model);
        }

        //
        // POST: /Role/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult Edit(RoleFormViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                if (ValidateRoleModel(model))
                {

                    model.UpdateRole();
                                       
                    
                }
                else
                {


                    throw new Exception();
                }
             
            }
            catch(Exception ex)
            {
                model.IsValidModel = false;
                if (model.MenuList == null)
                {
                    model.MenuList = new List<Menu>();
                }
                model.ApplicationId = model.Role.ApplicationId;
                model.ModuleId = model.Role.ModuleId;

                model.PopulateAppModSelectedList();
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
            }

           

            //model.PopulateSelectedList();
            return PartialView("Edit", model);
        }

       
        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public JsonResult GetModuleList(RoleFormViewModel model)
        {
            if (model.SearchCraiteria.ApplicationId <=0 && model.Role !=null)
            {

                model.SearchCraiteria.ApplicationId = model.Role.ApplicationId;
            }
            return Json(model.GetModuleListByApp(model.SearchCraiteria.ApplicationId));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult GetMenuList(RoleFormViewModel model)
        {


            try
            {
                //model.MenuList = this.GetMenusWithData();
                ModelState.Clear();
                model.ApplicationId = model.Role.ApplicationId= model.Role.ApplicationId == 0 ? -1 : model.Role.ApplicationId;
                model.ModuleId = model.Role.ModuleId = model.Role.ModuleId == 0 ? -1 : model.Role.ModuleId;

                //Populate Menulist by application and module id
                model.PopulateSelectedMenuList();

                //Populate Right List by application and Module
                model.PopulateRightList();
               
                //Populate Applicationa and module list to mange state
                model.PopulateAppModSelectedList();

            }
            catch
            {
                model.IsValidModel = false;
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
            }


            if (model.Role.RoleId > 0)
            {

                return PartialView("Edit", model);
            }
            else
            {
                return PartialView("Create", model);
            }
        }

        [NoCache]
        private bool ValidateRoleModel(RoleFormViewModel model)
        {
            ModelState.Clear();

            if (String.IsNullOrEmpty(model.Role.RoleName))
            {
                ModelState.AddModelError("Model.Role.RoleName", "The Role Name is a required field.");
            }

            if (model.Role.ApplicationId == 0)
            {
                ModelState.AddModelError("Model.Role.ApplicationId", "The Application is a required field.");
            }


            if (model.Role.ModuleId == 0)
            {
                ModelState.AddModelError("Model.Role.ModuleId", "The Module is a required field.");
            }    


            return ModelState.IsValid;
        }


    }
}
