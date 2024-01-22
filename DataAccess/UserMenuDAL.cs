using DBClient;
using Entity;
using PrimaryBaseLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserMenuDAL : DataAccessBase<UserMenu>
    {
        public override int Save(UserMenu item, DataSaveOption sOption)
        {
            int resutl = 0;
            try
            {
                //CustomParameterList paramList = this.GetSaveParamList(item, sOption);
                //DBHelper dbHelper = DBHelper.GetInstance();
                //dbHelper.ExecuteNonQuery(paramList, "uspSetUsersRoles");
                //resutl = dbHelper.IdentityValue;

            }
            catch (CaughtException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                CaughtException ce = new CaughtException(ex.Message, this, "Save");
                throw ce;
            }

            return resutl;

        }

        public System.Int32 SaveUserMenu(UserMenu userMenu, List<Menu> menuList, DataSaveOption sOption)
        {
            int userId = 0;
            try
            {
                IDbConnection con = DBHelper.GetConnection();
                con.Open();
                IDbTransaction transaction = DBHelper.GetTransaction(con);

                try
                {

                    DBHelper dbHelper = DBHelper.GetInstance();

                    //Save UsersMenu
                    Menu menuForDel = new Menu();
                    menuForDel.RoleId = userMenu.UserId;  //Here role id Used as a UserId
                    userId = userMenu.UserId;

                    CustomParameterList roleMenuParamList = this.GetUserMenuSaveParamList(menuForDel, DataSaveOption.Delete);
                    dbHelper.ExecuteNonQuery(roleMenuParamList, transaction, con, "uspSetUsersInMenus");
                    int roleMenuId = dbHelper.IdentityValue;

                    foreach (var item in menuList)
                    {
                        if (item.IsAssignedMenu && item.ParentMenuId != -1)
                        {
                            Menu menu = new Menu();
                            var tId= userMenu.UserId;
                            menu.RoleId = tId;
                            menu.ApplicationId = userMenu.ApplicationId;
                            menu.ModuleId = item.ModuleId;
                            menu.MenuId = item.MenuId;
                            menu.IsAddAssigned = item.IsAddAssigned;
                            menu.IsEditAssigned = item.IsEditAssigned;
                            menu.IsDeleteAssigned = item.IsDeleteAssigned;
                            menu.IsCancelAssigned = item.IsCancelAssigned;
                            menu.IsPrintAssigned = item.IsPrintAssigned;
                            roleMenuParamList = this.GetUserMenuSaveParamList(menu, DataSaveOption.Insert);
                            dbHelper.ExecuteNonQuery(roleMenuParamList, transaction, con, "uspSetUsersInMenus");
                            roleMenuId = dbHelper.IdentityValue;
                        }
                    }
                    //

                    transaction.Commit();
                    con.Close();


                }
                catch (CaughtException ce)
                {
                    transaction.Rollback();
                    throw ce;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    CaughtException ce = new CaughtException(ex.Message, this, "Save");
                    throw ce;
                }
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

            return userId;

        }

        public override List<UserMenu> LoadItemList(UserMenu item, DataLoadOption lOption)
        {
            List<UserMenu> userMenuList = new List<UserMenu>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetAllUserMenu");

                foreach (DataRow dr in dt.Rows)
                {
                    UserMenu roleInfo = new UserMenu();
                    roleInfo.UserId = Convert.ToInt32(dr["UserId"].ToString());
                    roleInfo.ModuleId = Convert.ToInt32(dr["ModuleId"].ToString());
                    userMenuList.Add(roleInfo);
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

            return userMenuList;
        }

        public override List<UserMenu> LoadItemList(UserMenu item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override UserMenu LoadItem(UserMenu item, DataLoadOption lOption)
        {
            throw new NotImplementedException();
        }

        public List<Menu> LoadUserMenuList(UserMenu item, DataLoadOption lOption)
        {

            List<Menu> userMenuList = new List<Menu>();

            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transaction = DBHelper.GetTransaction(con);

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetUsersInMenus");

                foreach (DataRow dr in dt.Rows)
                {
                    Menu menu = new Menu();
                    MapperBase.GetInstance().MapItem(menu, dr);
                    userMenuList.Add(menu);
                }

                transaction.Commit();
                con.Close();
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                CaughtException ce = new CaughtException(ex.Message, this, "LoadUserMenuList");
                throw ce;
            }

            return userMenuList;
        }

        private CustomParameterList GetUserMenuSaveParamList(Menu item, DataSaveOption sOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@SaveOption", sOption, DbType.Int32));
            paramList.Add(new CustomParameter("@UserId",item.RoleId, DbType.Int32));
            paramList.Add(new CustomParameter("@ModuleId", item.ModuleId, DbType.Int32));
            paramList.Add(new CustomParameter("@MenuId", item.MenuId, DbType.Int32));
            paramList.Add(new CustomParameter("@IsAddAssigned", item.IsAddAssigned, DbType.Boolean));
            paramList.Add(new CustomParameter("@IsEditAssigned", item.IsEditAssigned, DbType.Boolean));
            paramList.Add(new CustomParameter("@IsDeleteAssigned", item.IsDeleteAssigned, DbType.Boolean));
            paramList.Add(new CustomParameter("@IsCancelAssigned", item.IsCancelAssigned, DbType.Boolean));
            paramList.Add(new CustomParameter("@IsPrintAssigned", item.IsPrintAssigned, DbType.Boolean));


            return paramList;

        }

        private CustomParameterList GetLoadParamList(UserMenu item, DataLoadOption lOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            paramList.Add(new CustomParameter("@UserId", item.UserId, DbType.Int32));
            paramList.Add(new CustomParameter("@ModuleId", item.ModuleId, DbType.Int32));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.Int32));
            return paramList;
        }


        public static UserMenuDAL GetInstance()
        {
            return new UserMenuDAL();
        }


    }
}
