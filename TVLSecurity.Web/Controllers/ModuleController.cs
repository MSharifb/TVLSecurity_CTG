using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TVLSecurity.Web.Models;
using Entity;
using TVLSecurity.Web.Code;
using PrimaryBaseLibrary;

namespace TVLSecurity.Web.Controllers
{
    [NoCache]
    public class ModuleController : Controller
    {
        //
        // GET: /Module/
        [NoCache]
        public ActionResult Index()
        {
            ViewData["PageTitle"] = "Module";
            ModuleFormViewModel model = new ModuleFormViewModel();          
            return View(model);
        }
        


        [AcceptVerbs(HttpVerbs.Get)]
        [NoCache]
        public ActionResult List(int? page)
        {
            ModelState.Clear();
            ModuleFormViewModel model = new ModuleFormViewModel();
            model.LoadAllModules();


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            if (model.ModuleList.Count > 0)
            {
               // model.ModulePagedList = model.ModuleList.ToPagedList(currentPageIndex, 10);               
            }
            else
            {
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.NoDataFound);
                model.NoDataFound = true;
            }

            return PartialView("List", model);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult List(int? page, ModuleFormViewModel model)
        {
            ModelState.Clear();          
            model.LoadAllModules();
        
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            if (model.ModuleList.Count > 0)
            {
               // model.ModulePagedList = model.ModuleList.ToPagedList(currentPageIndex, 10);
               
            }
            else
            {
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.NoDataFound);
                model.NoDataFound = true;
            }

            return PartialView("List", model);
        }

        //
        // GET: /Module/Details/5
        [NoCache]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Module/Create
        [NoCache]
        public ActionResult Create()
        {
            ModuleFormViewModel model = new ModuleFormViewModel();

            return PartialView("Create", model);
        } 


        //
        // POST: /Module/Create

        [HttpPost]
        [NoCache]
        public ActionResult Create(ModuleFormViewModel model)
        {
            try
            {
                if (ValidateModuleInformation(model))
                {

                   int returnValue =  model.CreateModule();
                   if (returnValue > 0)
                   {
                       model.Message = Messages.GetMessage(Messages.MessageIdEnum.SavedSuccessFully);
                       model.IsValidModel = true;
                       model.Module = new Module();
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
                    model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
                    model.IsValidModel = false;
                }


            }
            catch
            {
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
                model.IsValidModel = false;
            }

            return PartialView("Create", model);

        }
        
        //
        // GET: /Module/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            ModuleFormViewModel model = new ModuleFormViewModel();
            try
            {

                model.LoadModuleById(id);

            }
            catch (Exception)
            {
                model.IsValidModel = false;
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.NotAbleToLoad);
            }
            ModelState.Clear();
            HttpContext.Response.Clear();
            return PartialView("Edit", model);
        }

        //
        // POST: /Application/Edit/5

        [HttpPost]
        [NoCache]
        public ActionResult Edit(ModuleFormViewModel model)
        {
            try
            {
                if (ValidateModuleInformation(model))
                {

                  int returnValue =   model.UpdateModule();
                  if (returnValue > 0)
                  {
                      model.Message = Messages.GetMessage(Messages.MessageIdEnum.UpdatedSuccessFully);
                      model.IsValidModel = true;
                  }
                  else
                  {
                      model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave).ToString() + "\n" + Messages.GetMessage((Messages.MessageIdEnum)returnValue).ToString();
                      model.IsValidModel = false;         
                  }

                }
                else
                {
                    model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
                    model.IsValidModel = false;
                }


            }
            catch
            {
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
                model.IsValidModel = false;
            }

            return PartialView("Edit", model);
        }


        //
        // GET: /Module/Delete/5
        [NoCache]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Module/Delete/5

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

        [NoCache]
        private bool ValidateModuleInformation(ModuleFormViewModel model)
        {

            ModelState.Clear();
            if (String.IsNullOrEmpty(model.Module.ModuleName))
            {
                ModelState.AddModelError("Model.Module.ModuleName", "The Module Name is a required field.");
            }

            if (string.IsNullOrEmpty(model.Module.ModuleTitle))
            {
                ModelState.AddModelError("Model.Module.ModuleTitle", " The Module Title is a required field.");
            }

            if (model.Module.ApplicationId <=0)
            {
                ModelState.AddModelError("Model.Module.ApplicationId", " The Application is a required field.");
            }
            return ModelState.IsValid;
        }
    }
}
