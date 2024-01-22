using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TVLSecurity.Business;
using Entity;
using DataAccess;
using PrimaryBaseLibrary;

namespace TVLSecurity.Web.Models
{
    public class UserFormViewModel : ModelFormViewModelBase
    {
        public MenuDAL objMenuDAL = new MenuDAL();

        public string strSearchEmpID { set; get; }
        public string strSearchEmpName { set; get; }
        public string strSearchDepartmentID { set; get; }       
        public int? ZoneId { get; set; }

        public Employee EmpSearchCraiteria { set; get; }

        public User User { set; get; }
        //public IPagedList<User> UserList { set; get; }
        public IList<User> UserList { set; get; }
        public List<SelectListItem> GroupList { set; get; }
        public List<SelectListItem> ZoneList { set; get; }
        public List<SelectListItem> DepartmentList { set; get; }
        public List<Role> RoleList { set; get; }
        public string ConfirmPassword { set; get; }
        public User SearchCraiteria { set; get; }
        public IList<Employee> EmployeeList { set; get; }
        public List<Zone> SelectedZoneList { set; get; }
        public List<Zone> GetZoneList { set; get; }
        public string OldPassword { set; get; }

        public SelectList SearchZoneList { set; get; }

        public List<Menu> MenuList { set; get; }
        public SelectList MenuModuleList { set; get; }
        public List<Right> RightList { set; get; }



        public UserFormViewModel()
        {
            User = new User();
            SearchCraiteria = new User();
            EmpSearchCraiteria = new Employee();
            IsValidModel = true;

        }


        // User in Menus

        public void PopulateSelectedMenuList()
        {

            Menu item = new Menu();
            item.ApplicationId = this.ApplicationId;
            item.ModuleId = this.ModuleId;

            this.MenuList = objMenuDAL.LoadItemList(item, DataLoadOption.LoadByCraiteria);

            List<Menu> menuList = new List<Menu>();

            foreach (Menu menu in this.MenuList)
            {
                if (menu.MenuName != "Application" && menu.MenuName != "Menu" && menu.MenuName != "Module" && menu.MenuName != "SystemInitialization")
                {
                    menuList.Add(menu);
                }
            }

            this.MenuList = menuList;


            List<Module> modList = new List<Module>();
            List<Module> modListItems = UserRepository.GetAllModules();
            if (modListItems.Count > 0)
            {
                foreach (var mod in modListItems)
                {
                    Module moduleItem = new Module();
                    moduleItem.ModuleTitle = mod.ModuleTitle;
                    moduleItem.ModuleId = mod.ModuleId;
                    modList.Add(moduleItem);
                }
            }
            this.MenuModuleList = new SelectList(modList, "ModuleId", "ModuleTitle", this.ModuleId.ToString());
        }
        public void PopulateRightList()
        {

            this.RightList = UserRepository.GetRightByRoleAndAppAndModule(0 , ApplicationId, ModuleId);
        }



    }
}
