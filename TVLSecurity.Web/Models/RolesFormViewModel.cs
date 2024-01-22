using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using TVLSecurity.Web.Code;
using DataAccess;
using PrimaryBaseLibrary;


namespace TVLSecurity.Web.Models
{
    public class RoleFormViewModel : ModelFormViewModelBase
    {
        public RoleDAL objRoleDAL=new RoleDAL();
        public MenuDAL objMenuDAL = new MenuDAL();

        public Role Role { set; get; }
        //public IPagedList<Role> RolePagedList { set; get; }
        public IList<Role> RoleList { set; get; }
        public List<Menu> MenuList { set; get; }
        public List<Right> RightList { set; get; }
        public Role SearchCraiteria { set; get; }


        public RoleFormViewModel()
        {
            //Role = new Role();
        }


        public RoleFormViewModel(bool initialize)
        {
            if (initialize)
            {

                Role = new Role();
                SearchCraiteria = new Role();
                MenuList = new List<Menu>();
                RightList = new List<Right>();
            }
        }

        public RoleFormViewModel(int roleId)
        {
            Role = UserRepository.GetRoleById(roleId);

            this.MenuList = UserRepository.GetMenusByApplicationAndModuleId(roleId, Role.ApplicationId, Role.ModuleId);
            List<Menu> menuList = new List<Menu>();
            foreach (Menu menu in this.MenuList)
            {
                if (menu.MenuName != "Application" && menu.MenuName != "Menu" && menu.MenuName != "Module" && menu.MenuName != "SystemInitialization")
                {
                    menuList.Add(menu);
                }
            }

            this.MenuList = menuList;

            SearchCraiteria = new Role();

            ApplicationId = Role.ApplicationId;
            ModuleId = Role.ModuleId;

            RightList = UserRepository.GetRightByRoleAndAppAndModule(roleId, ApplicationId, ModuleId);

            PopulateAppModSelectedList();
        }

        public void CreateRole()
        {
            //model.Role.ApplicationId = 1;
            try
            {
                //int returnValue = UserRepository.InsertRole(Role, MenuList, RightList);

                var returnValue = objRoleDAL.SaveRoleMenuRight(Role, MenuList, RightList, DataSaveOption.Insert);

                if (returnValue > 0)
                {
                    IsValidModel = true;
                    Message = Messages.GetMessage(Code.Messages.MessageIdEnum.SavedSuccessFully);

                    Role = new Role();
                    MenuList = new List<Menu>();

                }
                else
                {
                    Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave).ToString() + "\n" + Messages.GetMessage((Messages.MessageIdEnum)returnValue).ToString();
                    IsValidModel = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            PopulateAppModSelectedList();

        }

        public void UpdateRole()
        {
            try
            {
                //int returnValue = UserRepository.UpdateRole(Role, MenuList, RightList);

                int returnValue = objRoleDAL.SaveRoleMenuRight(Role, MenuList, RightList, DataSaveOption.Update);

                if (returnValue > 0)
                {

                    IsValidModel = true;
                    Message = Messages.GetMessage(Code.Messages.MessageIdEnum.UpdatedSuccessFully);
                }
                else
                {
                    Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave).ToString() + "\n" + Messages.GetMessage((Messages.MessageIdEnum)returnValue).ToString();
                    IsValidModel = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ApplicationId = Role.ApplicationId;
            ModuleId = Role.ModuleId;
            PopulateAppModSelectedList();

        }

        public void PopulateRoleList()
        {
            //
            IList<Role> roleList = UserRepository.GetRoleListByCraiteria(this.SearchCraiteria).OrderBy(x=>x.RoleName).ToList();
            //
            this.RoleList = roleList;
            //
            if (roleList.Count > 0)
            {
                
               // RoleList = roleList.ToPagedList(this.CurrentPageIndex, 20);
              
                IsValidModel = true;

            }
            else
            {
                Message = Messages.GetMessage(Messages.MessageIdEnum.NoDataFound);
                IsValidModel = false;
                NoDataFound = true;
            }

            this.ApplicationId = SearchCraiteria.ApplicationId;
            this.ModuleId = SearchCraiteria.ModuleId;

            PopulateAppModSelectedList();
        }

        public void PopulateMenuList()
        {
            //model.Role = new Role();
            this.MenuList = UserRepository.GetAllMenus();

        }

        public void PopulateSelectedMenuList()
        {
            //model.Role = new Role();
            //this.MenuList = UserRepository.GetMenusByApplicationAndModuleId(this.Role.RoleId, this.Role.ApplicationId, this.Role.ModuleId);

            Menu item = new Menu();
            //item.MenuId = this.MenuId;
            item.ApplicationId = this.ApplicationId;
            item.ModuleId = this.ModuleId;
            item.RoleId = this.Role.RoleId;


            this.MenuList = objMenuDAL.LoadItemList(item, DataLoadOption.LoadByCraiteria); 

            List<Menu> menuList = new List<Menu>();

            //if (String.Compare(HttpContext.Current.User.Identity.Name, TVLSecurity.Web.Code.AppConstraint.SystemInitializer, true) != 0)
            //{
                foreach (Menu menu in this.MenuList)
                {
                    if (menu.MenuName != "Application" && menu.MenuName != "Menu" && menu.MenuName != "Module" && menu.MenuName != "SystemInitialization")
                    {
                        menuList.Add(menu);
                    }
                }

                this.MenuList = menuList;
            //}

        }

        public void PopulateRightList()
        {

            this.RightList = UserRepository.GetRightByRoleAndAppAndModule(this.Role.RoleId, ApplicationId, ModuleId);
        }


        //public SelectList GetModuleListByApp(int applicationId)
        //{
        //    List<Module> moduleList = new List<Module>();
        //    Module module = new Module();
        //    module.ModuleTitle = "Select One";
        //    module.ModuleId = 0;


        //    if (applicationId > 0)
        //    {
        //        moduleList = UserRepository.GetModulesByApp(applicationId);
        //    }

        //    string strSelectedValue = "";
        //    if (moduleList.Count > 0)
        //    {
        //        strSelectedValue = moduleList[0].ModuleId.ToString();
        //    }
        //    moduleList.Insert(0, module);


        //    return new SelectList(moduleList, "ModuleId", "ModuleTitle");


        //}

        //public void PopulateSelectedList()
        //{
        //    if (SearchCraiteria != null && Role == null)
        //    {
        //        Role = new Role();
        //        Role.ApplicationId = SearchCraiteria.ApplicationId;
        //        Role.ModuleId = SearchCraiteria.ModuleId;
        //    }

        //    ApplicationList = new SelectList(UserRepository.GetAllApplications(), "ApplicationId", "ApplicationTitle", Role.ApplicationId.ToString());

        //    ModuleList = new SelectList(UserRepository.GetModulesByApp(Role.ApplicationId), "ModuleId", "ModuleTitle", Role.ModuleId.ToString());



        //}
    }
}
