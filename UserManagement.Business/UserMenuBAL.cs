using DataAccess;
using Entity;
using PrimaryBaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Business
{
    public class UserMenuBAL : BusinessBase<UserMenu>
    {
        public override int InsertData(UserMenu item)
        {
            throw new NotImplementedException();
        }

        public Int32 InsertUserMenus(UserMenu userMenu, List<Menu> menuList)
        {
            return UserMenuDAL.GetInstance().SaveUserMenu(userMenu, menuList, DataSaveOption.Insert);
        }

        public Int32 UpdateUserMenus(UserMenu userMenu, List<Menu> menuList)
        {
            return UserMenuDAL.GetInstance().SaveUserMenu(userMenu, menuList, DataSaveOption.Update);
        }

        public override int UpdateData(UserMenu item)
        {
            throw new NotImplementedException();
        }

        public override int DeleteData(int Id)
        {
            throw new NotImplementedException();
        }

        public override List<UserMenu> LoadAllItemList()
        {
            throw new NotImplementedException();
        }

        public override List<UserMenu> LoadItemList(UserMenu item)
        {
            return UserMenuDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadAll);
        }

        public override List<UserMenu> LoadItemList(UserMenu item, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override UserMenu LoadItem(int id)
        {
            throw new NotImplementedException();
        }

        public List<Menu> LoadUserMenuList(UserMenu userMenu)
        {
            return UserMenuDAL.GetInstance().LoadUserMenuList(userMenu, DataLoadOption.LoadByCraiteria);
        }


        public static UserMenuBAL GetInstance()
        {
            return new UserMenuBAL();
        }
    }
}
