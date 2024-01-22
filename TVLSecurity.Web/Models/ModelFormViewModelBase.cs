using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using DataAccess;
using TVLSecurity.Web.Code;
using PrimaryBaseLibrary;

namespace TVLSecurity.Web.Models
{
    public class ModelFormViewModelBase : ModelMainBase
    {
        MenuDAL objMenuDAL = new MenuDAL();

        public int ApplicationId { set; get; }
        public int ModuleId { set; get; }
        public string MenuCaption { set; get; }
        public SelectList ModuleList { set; get; }
        public SelectList ApplicationList { set; get; }
       // public SelectList MenuCaptionList { set; get; }

        public ModelFormViewModelBase()
        {
            Message = null;
            NoDataFound = false;
            IsValidModel = true;
        }

        public SelectList GetModuleListByApp(int applicationId)
        {
            List<Module> moduleList = new List<Module>();
            Module module = new Module();
            module.ModuleTitle = "Select One";
            module.ModuleId = 0;
            
            if (applicationId > 0)
            {
                moduleList = UserRepository.GetModulesByApp(applicationId);
            }

            string strSelectedValue = "";
            if (moduleList.Count > 0)
            {
                strSelectedValue = moduleList[0].ModuleId.ToString();
            }
            moduleList.Insert(0, module);

            return new SelectList(moduleList, "ModuleId", "ModuleTitle");
        }

        //new Get Menu caption list by module id
        //public SelectList GetMenuCaptionListByModule(int ModuleId)
        //{
        //    List<Menu> MenuList = new List<Menu>();
        //    Menu menu = new Menu();
        //    menu.MenuCaption = "Select One";
        //    menu.MenuId = 0;
        //    menu.ModuleId = ModuleId;

        //    if (ModuleId > 0)
        //    {
        //        //MenuList = UserRepository.GetMenusByModuleId(ModuleId);

        //        MenuList = objMenuDAL.LoadItemList(menu, DataLoadOption.LoadByCraiteria);
        //    }

        //    string strSelectedValue = "";
        //    if (MenuList.Count > 0)
        //    {
        //        strSelectedValue = MenuList[0].MenuId.ToString();
        //    }
        //    MenuList.Insert(0, menu);

        //    return new SelectList(MenuList, "MenuId", "MenuCaption");
        //}

        public SelectList GetParentMenuListByApp(int moduleId)
        {
            List<Menu> menuList = new List<Menu>();         
            Menu  menu = new Menu();         
            menu.MenuCaption = "Select One";
            menu.MenuId = 0;

            if (moduleId > 0)
            {
                menuList = UserRepository.GetParentMenusByModuleId(moduleId);                
            }

            string strSelectedValue = "";
            if (menuList.Count > 0)
            {
                strSelectedValue = menuList[0].MenuId.ToString();
            }
           
            menuList.Insert(0, menu);
            return new SelectList(menuList, "MenuId", "MenuCaption");
        }


        public void PopulateAppModSelectedList()
        {
            ApplicationList = new SelectList(UserRepository.GetAllApplications(), "ApplicationId", "ApplicationTitle", this.ApplicationId.ToString());
            List<Module> modList = new List<Module>();

            if (modList.Count == 0)
            {
                Module module = new Module();
                module.ModuleTitle = "Select One";
                module.ModuleId = 0;
                modList.Add(module);
            }
            List<Module> modListItems = UserRepository.GetModulesByApp(this.ApplicationId);
            if (modListItems.Count > 0)
            {
                foreach (var item in modListItems)
                {
                    Module moduleItem = new Module();
                    moduleItem.ModuleTitle = item.ModuleTitle;
                    moduleItem.ModuleId = item.ModuleId;
                    modList.Add(moduleItem);
                }
            }
            ModuleList = new SelectList(modList, "ModuleId", "ModuleTitle", this.ModuleId.ToString());
        }

        //public void PopulateModMenuCaptionSelectedList()
        //{
        //    MenuCaptionList = new SelectList(UserRepository.GetModulesByApp(this.ApplicationId), "ModuleId", "ModuleTitle", this.ModuleId.ToString());
        //    List<Menu> menuList = UserRepository.GetParentMenusByModuleId(this.ModuleId);
        //    if (menuList.Count <= 0)
        //    {
        //        Menu menu = new Menu();
        //        menu.MenuCaption = "Select One";
        //        menu.MenuId = 0;
        //        menuList.Add(menu);
        //    }

        //    MenuCaptionList = new SelectList(menuList, "MenuId", "MenuCaption", this.MenuId.ToString());
        //}

        public SelectList GetDepartment()
        {
            List<Department> departmentList = new List<Department>();
            Department dept = new Department();
            dept.strDepartment = "Select One";
            dept.strDepartmentID = "";
            
            departmentList = UserRepository.GetAllDepartment();

            string strSelectedValue = "";
            if (departmentList.Count > 0)
            {
                strSelectedValue = departmentList[0].strDepartmentID.ToString();
            }
            departmentList.Insert(0, dept);

            return new SelectList(departmentList, "strDepartmentID", "strDepartment");
        }
    }
}
