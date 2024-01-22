using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TVLSecurity.Web.Models;
using System.Web.UI;
using Entity;
using System.Linq;
using System.Collections;
using System.Globalization;
using TVLSecurity.Web.Code;
using PrimaryBaseLibrary;

namespace TVLSecurity.Web.Controllers
{
    [NoCache]
    public class MenuController : Controller
    {

        #region Actions--------------

        /// <summary>
        /// Index. Return View
       /// </summary>
        /// <returns>Index</returns>
        [NoCache]
        public ActionResult Index()
        {
            ViewData["PageTitle"] = "Menu";
            return View();
        }

        /// <summary>
        /// Populate Menu List Getting
        /// </summary>
        /// <param name="page"></param>
        /// <returns>Populate Menu Partial List</returns>
        [NoCache]
        public ActionResult List(int? page)
        {
            MenuFormViewModel model = new MenuFormViewModel(true);
           // ModelState.Clear();
            model.CurrentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.PopulateMenuList();

            return PartialView("List", model);
        }

        /// <summary>
        /// Populate Menu List post
        /// </summary>
        /// <param name="page"></param>
        /// <param name="model"></param>
        /// <returns>Populate Menu and Return Partial List</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult List(int? page, MenuFormViewModel model)
        {

            ModelState.Clear();
            model.CurrentPageIndex = page.HasValue ? page.Value - 1 : 0;

            model.ApplicationId = model.SearchCraiteria.ApplicationId;
            model.ModuleId = model.SearchCraiteria.ModuleId;
            model.MenuCaption = model.SearchCraiteria.MenuCaption == null? "" : model.SearchCraiteria.MenuCaption;
            
            model.PopulateAppModSelectedList();
            //model.PopulateModMenuCaptionSelectedList();
            model.PopulateMenuList();

            return PartialView("List", model);
        }

        //
        // GET: /Menu/Create
        [NoCache]
        public ActionResult Create()
        {
            MenuFormViewModel model = new MenuFormViewModel(true);

            return PartialView("Create", model);
        } 

        
         //POST: /Menu/Create

        [HttpPost]
        [NoCache]
        public ActionResult Create(MenuFormViewModel model)
        {
            try
            {
                if (ValidateMenuModel(model))
                {
                   int returnValue =  model.Create();

                   if (returnValue > 0)
                   {
                      
                       model.Menu = new Menu();
                       ModelState.Clear();
                       model.Message = Messages.GetMessage(Messages.MessageIdEnum.SavedSuccessFully);
                      
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
            catch (Exception ex)
            {
                model.IsValidModel = false;
                model.ApplicationId = model.Menu.ApplicationId;
                model.ModuleId = model.Menu.ModuleId;

                model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
            }

            model.PopulateAppModSelectedList();
            model.PopulateMenuSelectList();
                                  
            return PartialView("Create", model);
        }

        
        // GET: /Menu/Edit/5

        [NoCache]
        public ActionResult Edit(int id)
        {
            MenuFormViewModel model = new MenuFormViewModel(true);
            try
            {
                ModelState.Clear();
                model.PopulateMenu(id);

               

            }
            catch (Exception)
            {
                model.IsValidModel = false;
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.NotAbleToLoad);
            }

            model.PopulateAppModSelectedList();
            model.PopulateMenuSelectList();

            return PartialView("Edit", model);
        }

        //
        // POST: /Menu/Edit/5

        [HttpPost]
        [NoCache]
        public ActionResult Edit(MenuFormViewModel model)
        {
            try
            {
                if (ValidateMenuInformation(model))
                {

                  int returnValue=  model.Update();

                  if (returnValue > 0)
                  {
                      model.IsValidModel = true;
                      model.ApplicationId = model.Menu.ApplicationId;
                      model.ModuleId = model.Menu.ModuleId;
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
            catch
            {
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
                model.IsValidModel = false;
                model.ApplicationId = model.Menu.ApplicationId;
                model.ModuleId = model.Menu.ModuleId;
            }

            model.PopulateAppModSelectedList();
            model.PopulateMenuSelectList();

            return PartialView("Edit", model);
        }

        //
        // GET: /Menu/Delete/5
        [NoCache]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Menu/Delete/5

        [HttpPost]
        [NoCache]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Utilities------------

        /// <summary>
        /// Get detail by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View</returns>
        [NoCache]
        public ActionResult Details(int id)
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public JsonResult GetModuleList(MenuFormViewModel model)
        {
            if (model.SearchCraiteria.ApplicationId <= 0)
            {
                if (model.Menu != null)
                {
                    model.SearchCraiteria.ApplicationId = model.Menu.ApplicationId;                    
                }
            }
            return Json(model.GetModuleListByApp(model.SearchCraiteria.ApplicationId));
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //[NoCache]
        //public JsonResult GetMenuCaptionList(MenuFormViewModel model)
        //{
        //    if (model.SearchCraiteria.ModuleId <= 0)
        //    {
        //        if (model.Menu != null)
        //        {
        //            model.SearchCraiteria.ModuleId = model.Menu.ModuleId;
        //        }
        //    }
        //    return Json(model.GetMenuCaptionListByModule(model.SearchCraiteria.ModuleId));
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public JsonResult GetParentMenuList(MenuFormViewModel model)
        {
            if (model.SearchCraiteria.ModuleId <= 0)
            {
                if (model.Menu != null)
                {
                    model.SearchCraiteria.ModuleId = model.Menu.ModuleId;
                }
            }
            return Json(model.GetParentMenuListByApp(model.SearchCraiteria.ModuleId));
        }

        /// <summary>
        /// Get Left Menu List
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [NoCache]
        public ActionResult LeftMenu()
        {

            MenuFormViewModel model = new MenuFormViewModel();           
            model.MenuList = UserRepository.GetMenus(User.Identity.Name.ToString(),AppConstraint.ApplicationName,AppConstraint.ModuleName);

            if (string.Compare(User.Identity.Name.Trim(), AppConstraint.SystemInitializer, true) != 0)
            {
                int intCount = model.MenuList.Count;
                int index= 0;

                while (index < intCount)
                {
                    if (string.Compare(model.MenuList[index].MenuName, "Menu", true) == 0 || string.Compare(model.MenuList[index].MenuName, "Application", true) == 0 || string.Compare(model.MenuList[index].MenuName, "Module", true) == 0)
                    {
                        model.MenuList.RemoveAt(index);
                        intCount--;
                    }
                    else
                    {
                        index++;
                    }
                }
                
            }

            model.MenuList = model.MenuList.OrderBy(m => m.MenuId).ToList();
            
            return PartialView(model);
        }

        [NoCache]
        private bool ValidateMenuInformation(MenuFormViewModel model)
        {

            ModelState.Clear();
            if (String.IsNullOrEmpty(model.Menu.MenuName))
            {
                ModelState.AddModelError("Model.Menu.MenuName", "The Menu Name is a required field.");
            }

            else
            {
                System.Text.RegularExpressions.Regex regAppName = new System.Text.RegularExpressions.Regex(@"^[A-Za-z](?=[A-Za-z0-9_.]{1,31}$)[a-zA-Z0-9_]*\.?[a-zA-Z0-9_]*$");

                if (!regAppName.IsMatch(model.Menu.MenuName))
                {
                    ModelState.AddModelError("Model.Menu.MenuName", "The Menu Name is not valid.Use 2 to 32 characters and start with a letter. You may use letters, numbers, underscores, and one dot (.)");
                }
            }

            if (String.IsNullOrEmpty(model.Menu.MenuCaption))
            {
                ModelState.AddModelError("Model.Menu.MenuCaption", "The Menu Caption is a required field.");
            }

        

            if (model.Menu.ApplicationId == 0)
            {
                ModelState.AddModelError("Model.Menu.ApplicationId", "The Application is a required field.");
            }


            if (model.Menu.ModuleId == 0)
            {
                ModelState.AddModelError("Model.Menu.ModuleId", "The Module is a required field.");
            }

            if (model.Menu.SerialNo <= 0)
            {
                ModelState.AddModelError("Model.Menu.SerialNo", "The Serial No. must be greater than zero.");
            }

          
            return ModelState.IsValid;
        }

        [NoCache]
        private bool ValidateMenuModel(MenuFormViewModel model)
        {
            ModelState.Clear();

            if (model.Menu.ApplicationId == 0)
            {
                ModelState.AddModelError("Model.Menu.ApplicationId", "The Application is a required field.");
            }


            if (model.Menu.ModuleId == 0)
            {
                ModelState.AddModelError("Model.Menu.ModuleId", "The Module is a required field.");
            }


            if (String.IsNullOrEmpty(model.Menu.MenuCaption))
            {
                ModelState.AddModelError("Model.Menu.MenuCaption", "The Menu Caption is a required field.");
            }

            if (String.IsNullOrEmpty(model.Menu.MenuName))
            {
                ModelState.AddModelError("Model.Menu.MenuName", "The Menu Name is a required field.");
            }

            if (model.Menu.SerialNo == 0)
            {
                ModelState.AddModelError("Model.Menu.SerialNo", "The SerialNo is a required field.");
            }

            

            return ModelState.IsValid;
        }

        #endregion
    }
}
