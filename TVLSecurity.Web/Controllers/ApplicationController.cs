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
    public class ApplicationController : Controller
    {
        //
        // GET: /Application/
        [NoCache]
        public ActionResult Index()
        {
            ViewData["PageTitle"] = "Application";
            return View();
        }

        //
        // GET: /Application/Details/5


        [AcceptVerbs(HttpVerbs.Get)]
        [NoCache]
        public PartialViewResult List(int? page)
        {
            ModelState.Clear();
            ApplicationFormViewModel model = new ApplicationFormViewModel();
            model.LoadAllApplications();


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            if (model.ApplicationList.Count > 0)
            {               
               // model.ApplicationPagedList = model.ApplicationList.ToPagedList(currentPageIndex, 5);              
               
            }
            else
            {
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.NoDataFound);
                model.NoDataFound = true;
            }

            return PartialView("_List", model);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public PartialViewResult List(int? page, ApplicationFormViewModel model)
        {
            ModelState.Clear();           
            model.LoadAllApplications();


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            if (model.ApplicationList.Count > 0)
            {
                //model.ApplicationPagedList = model.ApplicationList.ToPagedList(currentPageIndex, 5);
                return PartialView("List", model);
            }
            else
            {
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.NoDataFound);
                model.NoDataFound = true;
                model.IsValidModel = false;
                return PartialView("List", model);
            }
        }


        [NoCache]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Application/Create
        [NoCache]
        public PartialViewResult Create()
        {
            ApplicationFormViewModel model = new ApplicationFormViewModel();

            return PartialView("Create", model);
        } 

        //
        // POST: /Application/Create

        [HttpPost]
        [NoCache]
        public PartialViewResult Create(ApplicationFormViewModel model)
        {
            try
            {
                if (ValidateApplicationInformation(model))
                {

                    int returnValue = model.CreateApplication();


                    if (returnValue > 0)
                   {
                       model.Message = Messages.GetMessage(Messages.MessageIdEnum.SavedSuccessFully);
                       model.IsValidModel = true;
                       model.Application = new Application();
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
            catch(Exception ex)
            {
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
                model.IsValidModel = false;
            }

            return PartialView("Create", model);

        }
        
        //
        // GET: /Application/Edit/5
        [NoCache]
        public PartialViewResult Edit(int id)
        {
            ApplicationFormViewModel model = new ApplicationFormViewModel();
            try
            {
                
                model.LoadApplicatonById(id);                
                
            }
            catch (Exception)
            {
                model.IsValidModel = false;
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.NotAbleToLoad);
            }
            return PartialView("Edit", model);
        }

        //
        // POST: /Application/Edit/5

        [HttpPost]
        [NoCache]
        public PartialViewResult Edit(ApplicationFormViewModel model)
        {
            try
            {
                if (ValidateApplicationInformation(model))
                {

                    int returnValue = model.UpdateApplication();

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
                    throw new Exception();
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
        // GET: /Application/Delete/5
        [NoCache]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Application/Delete/5

        [HttpPost]
        [NoCache]
        public ActionResult Delete(ApplicationFormViewModel model)
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
        private bool ValidateApplicationInformation(ApplicationFormViewModel model)
        {

            ModelState.Clear();
            if (String.IsNullOrEmpty(model.Application.ApplicationName))
            {
                ModelState.AddModelError("Model.Application.ApplicationName", "The Application Name is a required field.");
            }
            else
            {
                System.Text.RegularExpressions.Regex regAppName = new System.Text.RegularExpressions.Regex(@"^[A-Za-z](?=[A-Za-z0-9_.]{1,31}$)[a-zA-Z0-9_]*\.?[a-zA-Z0-9_]*$");

                if (!regAppName.IsMatch(model.Application.ApplicationName))
                {
                    ModelState.AddModelError("Model.Application.ApplicationName", "The Application Name is not valid.Use 2 to 32 characters and start with a letter. You may use letters, numbers, underscores, and one dot (.)");
                }
            }

            if (string.IsNullOrEmpty(model.Application.ApplicationTitle))
            {
                ModelState.AddModelError("Model.Application.ApplicationTitle", " The Application Title is a required field.");
            }                     
            return ModelState.IsValid;
        }
    }
}
