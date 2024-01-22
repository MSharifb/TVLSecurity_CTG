using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;


namespace TVLSecurity.Web.Models
{
    public class ModuleFormViewModel : ModelMainBase
    {        
        public Module Module { set; get; }     
        public IList<Module> ModuleList { set; get; }
        public SelectList ApplicationList { set; get; }

        public ModuleFormViewModel()
        {
            Module = new Module();
            ApplicationList = new SelectList(UserRepository.GetAllApplications(), "ApplicationId", "ApplicationTitle");

        }
        
        public void LoadAllModules()
        {
            ModuleList = UserRepository.GetAllModules();
        }

        public void LoadModuleById(int id)
        {
            Module = UserRepository.GetModuleById(id);
            ApplicationList = new SelectList(this.ApplicationList.Items, "ApplicationId", "ApplicationTitle", Module.ApplicationId);

        }

        public int CreateModule()
        {
            return UserRepository.CreateModule(Module);
        }

        public int UpdateModule()
        {
            int returnValue = UserRepository.UpdateModule(Module);
            ApplicationList = new SelectList(this.ApplicationList.Items, "ApplicationId", "ApplicationName", Module.ApplicationId);
            return returnValue;
        }

        public void DeleteApplication()
        {

        }

    }
}