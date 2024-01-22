using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TVLSecurity.Web.Models;
using TVLSecurity.Web.Code;
using System.Web.UI;
using Entity;
using System.Collections;
using PrimaryBaseLibrary;


namespace TVLSecurity.Web.Controllers
{
    [NoCache]
    public class UserGroupController : Controller
    {
        //
        // GET: /UserGroup/
        [NoCache]
        public ActionResult Index()
        {
            ViewData["PageTitle"] = "User Group";
            UserGroupFormViewModel model = new UserGroupFormViewModel();
            model.GroupList = UserRepository.GetGroupList();

            return View(model);
        }

        //
        // GET: /UserGroup/Details/5
        [NoCache]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /UserGroup/Create
        [NoCache]
        public ActionResult Create()
        {
            UserGroupFormViewModel model = new UserGroupFormViewModel();
            model.Group = new UserGroup();
            model.RoleList = UserRepository.GetRoles();
            model.PopulateAppModSelectedList();
            return PartialView("Create", model);
        } 

        //
        // POST: /UserGroup/Create

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult Create(UserGroupFormViewModel model)
        {
            try
            {
                if (ValidateUserGroupModel(model))
                {
                    int returnValue = UserRepository.CreateGroup(model.Group, model.RoleList);

                    if (returnValue > 0)
                    {
                        model.Group.ApplicationId = model.ApplicationId;
                        model.Group.ModuleId = model.ModuleId;
                        model.IsValidModel = true;
                        model.Message = Messages.GetMessage(Messages.MessageIdEnum.SavedSuccessFully);
                        model.Group = new UserGroup();
                        ModelState.Clear();
                    }
                    else
                    {
                        model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave).ToString() + "\n" + Messages.GetMessage((Messages.MessageIdEnum)returnValue).ToString();
                        model.IsValidModel = false;
                    }
                }
                else
                {
                    throw new Exception();
                }
               
            }
            catch
            {
                model.IsValidModel= false;
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
            }

            model.PopulateAppModSelectedList();

            return PartialView("Create", model);

        }

        //
        // GET: /UserGroup/Edit/5
        //[NoCache]
        public ActionResult Edit(int id)
        {

            UserGroupFormViewModel model = new UserGroupFormViewModel();
            model.Group = UserRepository.GetUserGroupById(id);
            model.RoleList = UserRepository.GetRolesByUserGroup(id);
            model.PopulateAppModSelectedList();
            return PartialView("Edit",model);
        }

        //
        // POST: /UserGroup/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult Edit(UserGroupFormViewModel model)
        {
            try
            {
                if (ValidateUserGroupModel(model))
                {
                   int returnValue =  UserRepository.UpdateGroup(model.Group, model.RoleList);
                   if (returnValue > 0)
                   {
                       model.Group.ApplicationId = model.ApplicationId;
                       model.Group.ModuleId = model.ModuleId;

                       model.IsValidModel = true;
                       model.Message = Messages.GetMessage(Messages.MessageIdEnum.UpdatedSuccessFully);
                   }
                   else
                   {
                       model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave).ToString() + "\n" + Messages.GetMessage((Messages.MessageIdEnum)returnValue).ToString();
                       model.IsValidModel = false;
                   }


                }
                else
                {
                    throw new Exception();
                }

               
            }
            catch(Exception ex)
            {

                model.IsValidModel = false;
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
            }

            model.PopulateAppModSelectedList();
            return PartialView("Edit", model);
        }

        [NoCache]
        private bool ValidateUserGroupModel(UserGroupFormViewModel model)
        {
            ModelState.Clear();

            if (String.IsNullOrEmpty(model.Group.GroupName))
            {
                ModelState.AddModelError("model.Group.GroupName", "The Group Title is a required field.");
            }



            return ModelState.IsValid;
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult GetRoleList(UserGroupFormViewModel model)
        {
            try
            {


                Role role = new Role();
                role.ApplicationId = model.ApplicationId;
                role.ModuleId = model.ModuleId;
                role.UserGroupId = model.Group.GroupId;
                
                //Get Role List by searching craiteria(Application/Module)
                model.RoleList = UserRepository.GetRoleListByCraiteria(role);
                
                //populate application and module list
                model.PopulateAppModSelectedList();

                //set Model is valid
                model.IsValidModel = true;
                ModelState.Clear();




            }
            catch
            {
                model.IsValidModel = false;
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.NotAbleToLoad);
            }

            if (model.Group.GroupId <= 0)
            {
                return PartialView("Create", model);
            }
            else
            {
                return PartialView("Edit", model);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public JsonResult GetModuleList(UserFormViewModel model)
        {

            return Json(model.GetModuleListByApp(model.ApplicationId));
        }
    }
}
