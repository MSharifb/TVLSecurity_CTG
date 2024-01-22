using System;
using Entity;
using DataAccess;
using System.Collections.Generic;
using PrimaryBaseLibrary;

namespace UserManagement.Business
{
    public class MenuBAL : BusinessBase<Menu>
    {
        public override int InsertData(Menu item)
        {
            return MenuDAL.GetInstance().Save(item, DataSaveOption.Insert);
        }

        public override int UpdateData(Menu item)
        {
            return MenuDAL.GetInstance().Save(item, DataSaveOption.Update);
        }

        public override int DeleteData(int Id)
        {
            Menu item = new Menu();
            item.MenuId = Id;
            return MenuDAL.GetInstance().Save(item, DataSaveOption.Delete);
        }

        public override List<Menu> LoadAllItemList()
        {
            return MenuDAL.GetInstance().LoadItemList(new Menu(), DataLoadOption.LoadByCraiteria);
        }

        public  List<Menu> LoadAllItemListByRoleId(int roleId)
        {
            Menu menu = new Menu();
            menu.RoleId = roleId;
            return MenuDAL.GetInstance().LoadItemList(menu, DataLoadOption.LoadByCraiteria);
        }

        public override List<Menu> LoadItemList(Menu item)
        {
         
           return MenuDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadByCraiteria);
                       
             
        }

        public  Menu LoadItem(Menu item)
        {

            List<Menu> menuList = MenuDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadByCraiteria);
            if (menuList.Count > 0)
            {
                return menuList[0];
            }
            return null;


        }

        public  List<Menu> LoadMenuListByParent(int parentId)
        {

            Menu menu = new Menu();
            menu.ParentMenuId = parentId;
            return MenuDAL.GetInstance().LoadItemList(menu, DataLoadOption.LoadByCraiteria);          

        }

        public List<Menu> LoadMenuListByLoginId(string loginId)
        {

            Menu menu = new Menu();
            menu.LoginId = loginId;
            menu.ParentMenuId = -1;
            return MenuDAL.GetInstance().LoadItemList(menu, DataLoadOption.LoadByCraiteria);

        }

        public override List<Menu> LoadItemList(Menu item, int startRowIndex, int maxRow)
        {
            return MenuDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadByCraiteria);
        }

        public override Menu LoadItem(int id)
        {
            throw new NotImplementedException();
        }

        public static MenuBAL GetInstance()
        {
            return new MenuBAL();
        }
    }
}
