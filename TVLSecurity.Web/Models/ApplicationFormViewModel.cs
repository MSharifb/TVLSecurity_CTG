using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;


namespace TVLSecurity.Web.Models
{
    public class ApplicationFormViewModel : ModelFormViewModelBase
    {
       public  Application Application { set; get; }
       public  Module Module { set; get; }
       //public  IPagedList<Application> ApplicationPagedList { set; get; }
       public IList<Application> ApplicationList { set; get; }

        public ApplicationFormViewModel()
        {
            Application = new Application();
            Module = new Module();
            List<Application> ApplicationList = new List<Application>();
            IsValidModel = true;
        }

        public void LoadAllApplications()
        {
            this.ApplicationList = UserRepository.GetAllApplications();
        }

        public void LoadApplicatonById(int id)
        {
            this.Application = UserRepository.GetApplicationById(id);
        }

        public int CreateApplication()
        {
           return UserRepository.CreateApplication(this.Application);
        }

        public int UpdateApplication()
        {
           return UserRepository.UpdateApplication(this.Application);
        }

        public void DeleteApplication()
        {
           
        }


      

    }
}