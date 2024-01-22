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
    public class MenuFormViewModel : ModelFormViewModelBase
    {
       MenuDAL objMenuDAL = new MenuDAL();

       public Menu Menu { set; get; }
       public IList<Menu>  MenuList { set; get; }
       //public IPagedList<Menu> MenuPagedList { set; get; }
       public SelectList MenuSelectList { set; get; }
       public Menu SearchCraiteria { set; get; }


        public MenuFormViewModel()
        {
            
        }

        public MenuFormViewModel(bool initialize)
        {
            if (initialize)
            {
                Menu = new Menu();
                SearchCraiteria = new Entity.Menu();
                PopulateAppModSelectedList();
                //PopulateModMenuCaptionSelectedList();
                PopulateMenuSelectList();   
            }
        }

        public int Create()
        {
            
            //return  UserRepository.InsertMenuData(this.Menu);
            return objMenuDAL.Save(this.Menu, DataSaveOption.Insert);
        }

        public int Update()
        {
           //return  UserRepository.UpdateMenuData(this.Menu);
            return objMenuDAL.Save(this.Menu, DataSaveOption.Update);
        }

        public void Delete()
        {
        }

        public void PopulateMenuList()
        {
            //this.MenuList = UserRepository.GetMenusByApplicationAndModuleId(this.MenuId, this.ApplicationId, this.ModuleId);

            Menu item = new Menu();
            item.MenuCaption = this.MenuCaption;
            item.ApplicationId = this.ApplicationId;
            item.ModuleId = this.ModuleId;

            this.MenuList = objMenuDAL.LoadItemList(item, DataLoadOption.LoadByCraiteria);

           if (this.MenuList.Count > 0)
           {
               //this.MenuPagedList = this.MenuList.OrderBy(m=>m.MenuId).ToPagedList(CurrentPageIndex,20);   
               this.IsValidModel = true;
           }
           else
           {
               this.IsValidModel = false;
               this.NoDataFound = true;
               this.Message = Messages.GetMessage(Messages.MessageIdEnum.NoDataFound);
           }
        }

        public void PopulateMenuList(string loginId)
        {
            this.MenuList = UserRepository.GetMenuListByLoginId(loginId);

            if (this.MenuList.Count > 0)
            {
                //this.MenuPagedList = this.MenuList.OrderBy(m => m.MenuId).ToPagedList(CurrentPageIndex, 5);
                this.IsValidModel = true;
            }
            else
            {
                this.IsValidModel = false;
                this.NoDataFound = true;
                this.Message = Messages.GetMessage(Messages.MessageIdEnum.NoDataFound);
            }
        }

        public void PopulateMenuSelectList()
        {
            List<Menu> menuList = UserRepository.GetAllMenus();
            menuList = menuList.Where(x => x.ModuleId == Menu.ModuleId).ToList();
            Entity.Menu menu = new Menu();
            menu.MenuId = -1;
            menu.MenuCaption = "Select One";

            menuList.Insert(0,menu);

            MenuSelectList = new SelectList(menuList, "MenuId", "MenuCaption", Menu.ParentMenuId.ToString());
        }

        public void PopulateMenu(int id)
        {
            Menu item = new Menu();
            item.MenuId = id;

            this.Menu = objMenuDAL.LoadItemList(item, DataLoadOption.LoadByCraiteria).First();

            //this.Menu = UserRepository.GetMenusByMenuId(id);

            this.ApplicationId = this.Menu.ApplicationId;
            this.ModuleId = this.Menu.ModuleId;
        }

        public string MenuName { get; set; }
    }
}