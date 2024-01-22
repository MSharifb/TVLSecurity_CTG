using System;
using Entity;
using DataAccess;
using System.Collections.Generic;
using PrimaryBaseLibrary;

namespace UserManagement.Business
{
   public class RoleBAL : BusinessBase<Role>
    {

        public override int InsertData(Role item)
        {
            throw new NotImplementedException();
        }

        public Int32 InsertRoleMnuRight(Role role, List<Menu> menuList, List<Right> rightList)
        {
          return  RoleDAL.GetInstance().SaveRoleMenuRight(role, menuList, rightList, DataSaveOption.Insert);
        }

        public Int32 UpdateRoleMnuRight(Role role, List<Menu> menuList, List<Right> rightList)
        {
            return RoleDAL.GetInstance().SaveRoleMenuRight(role, menuList, rightList, DataSaveOption.Update);
        }
        public override int UpdateData(Role item)
        {
            throw new NotImplementedException();
        }

        public override int DeleteData(int Id)
        {
            throw new NotImplementedException();
        }

        public override List<Role> LoadAllItemList()
        {
            return RoleDAL.GetInstance().LoadItemList(new Role(), DataLoadOption.LoadAll);
        }

        public List<Role> LoadtemListByUser(int userId)
        {
            Role newRole = new Role();
            newRole.UserId = userId;
            return RoleDAL.GetInstance().LoadItemList(newRole, DataLoadOption.LoadAll);
        }

        public override List<Role> LoadItemList(Role item)
        {
            return RoleDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadByCraiteria);
        }

        public override List<Role> LoadItemList(Role item, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Role LoadItem(int id)
        {
            Role newRole = new Role();
            newRole.RoleId = id;
            return RoleDAL.GetInstance().LoadItem(newRole, DataLoadOption.LoadByCraiteria);
        }

        public static RoleBAL GetInstance()
        {
            return new RoleBAL();
        }
    }
}
