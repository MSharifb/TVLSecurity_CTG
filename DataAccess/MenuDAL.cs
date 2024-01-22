using System;
using System.Collections.Generic;
using Entity;
using System.Data;
using PrimaryBaseLibrary;
using DBClient;

namespace DataAccess
{
    public class MenuDAL : DataAccessBase<Menu>
    {
              
        public override int Save(Menu item, DataSaveOption sOption)
        {
            int id = 0;
            try
            {
                CustomParameterList paramList = this.GetSaveParamList(item, sOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                dbHelper.ExecuteNonQuery(paramList, "uspSetMenu");
                id = dbHelper.IdentityValue;

            }
            catch (CaughtException ce)
            {
                return ce.ErrorCode * -1;
            }
            catch (Exception ex)
            {
                CaughtException ce = new CaughtException(ex.Message, this, "Save");
                throw ce;
            }

            return id;
        }

        public override List<Menu> LoadItemList(Menu item, DataLoadOption lOption)
        {
            List<Menu> menuList = new List<Menu>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetMenu");

                foreach (DataRow dr in dt.Rows)
                {
                    Menu menu = new Menu();
                    MapperBase.GetInstance().MapItem(menu, dr);
                    //if (menu.RoleId > 0)
                    //{
                    //    menu.IsAssignedMenu = true;
                    //}
                    menuList.Add(menu);
                }
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                CaughtException ce = new CaughtException(ex.Message, this, "LoadItemList");
                throw ce;
            }

            return menuList;
  
        }

        public override List<Menu> LoadItemList(Menu item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Menu LoadItem(Menu item, DataLoadOption lOption)
        {
            throw new NotImplementedException();
        }


        private CustomParameterList GetSaveParamList(Menu item, DataSaveOption sOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@SaveOption", sOption, DbType.Int32));
            paramList.Add(new CustomParameter("@MenuId", item.MenuId, DbType.Int32));
            paramList.Add(new CustomParameter("@MenuName", item.MenuName, DbType.String));
            paramList.Add(new CustomParameter("@MenuCaption", item.MenuCaption, DbType.String));
            paramList.Add(new CustomParameter("@MenuCaptionBng", item.MenuCaptionBng, DbType.String));
            paramList.Add(new CustomParameter("@ParentMenuId", item.ParentMenuId, DbType.Int32));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.Int32));
            paramList.Add(new CustomParameter("@ModuleId", item.ModuleId, DbType.Int32));
            paramList.Add(new CustomParameter("@SerialNo", item.SerialNo, DbType.Int32));
            paramList.Add(new CustomParameter("@PageUrl", item.PageUrl, DbType.String));
                    
            return paramList;

        }

        private CustomParameterList GetLoadParamList(Menu item, DataLoadOption lOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            paramList.Add(new CustomParameter("@MenuId", item.MenuId, DbType.Int32));
            paramList.Add(new CustomParameter("@ParentMenuId", item.ParentMenuId, DbType.Int32));
            paramList.Add(new CustomParameter("@ModuleId", item.ModuleId, DbType.Int32));
            paramList.Add(new CustomParameter("@MenuName", item.MenuName, DbType.Int32));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.Int32));
            paramList.Add(new CustomParameter("@RoleId", item.RoleId, DbType.Int32));
            paramList.Add(new CustomParameter("@LoginId", item.LoginId, DbType.String));
            paramList.Add(new CustomParameter("@ApplicationName", item.ApplicationName, DbType.String));
            paramList.Add(new CustomParameter("@ModuleName", item.ModuleName, DbType.String));
            paramList.Add(new CustomParameter("@MenuCaption", item.MenuCaption, DbType.String));
            return paramList;
        }

        public static MenuDAL GetInstance()
        {
            return new MenuDAL();
        }       
    }
}
