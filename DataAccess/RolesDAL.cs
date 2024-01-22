using System;
using System.Collections.Generic;
using Entity;
using System.Data;
using PrimaryBaseLibrary;
using DBClient;

namespace DataAccess
{
    public class RoleDAL : DataAccessBase<Role>
    {
        public override System.Int32 Save(Role item, DataSaveOption sOption)
        {
            int id = 0;
            try
            {
                CustomParameterList paramList = this.GetSaveParamList(item, sOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                dbHelper.ExecuteNonQuery(paramList, "uspSetRole");
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


        public System.Int32 SaveRoleMenuRight(Role role, List<Menu> menuList, List<Right> rightList, DataSaveOption sOption)
        {
            int roleId = 0;
            try
            {
                IDbConnection con = DBHelper.GetConnection();
                con.Open();
                IDbTransaction transaction = DBHelper.GetTransaction(con);

                try
                {

                    DBHelper dbHelper = DBHelper.GetInstance();
                    //Save/Update Role
                    CustomParameterList paramList = this.GetSaveParamList(role, sOption);
                    dbHelper.ExecuteNonQuery(paramList, transaction, con, "uspSetRole");
                    roleId = dbHelper.IdentityValue;

                    //Save RolesMenu
                    Menu menuForDel = new Menu();
                    menuForDel.RoleId = roleId;
                    //menuForDel.ApplicationId = role.ApplicationId;
                    //menuForDel.ModuleId = role.ModuleId;

                    CustomParameterList roleMenuParamList = this.GetRoleMenuSaveParamList(menuForDel, DataSaveOption.Delete);
                    dbHelper.ExecuteNonQuery(roleMenuParamList, transaction, con, "uspSetMenusInRoles");
                    int roleMenuId = dbHelper.IdentityValue;

                    foreach (Menu menu in menuList)
                    {
                        if (menu.IsAssignedMenu && menu.ParentMenuId != -1)
                        {
                            menu.RoleId = roleId;
                            roleMenuParamList = this.GetRoleMenuSaveParamList(menu, DataSaveOption.Insert);
                            dbHelper.ExecuteNonQuery(roleMenuParamList, transaction, con, "uspSetMenusInRoles");
                            roleMenuId = dbHelper.IdentityValue;
                        }
                    }
                    //

                    //Save RolesRight
                    Right rightForDel = new Right();
                    rightForDel.RoleId = roleId;
                    CustomParameterList roleRightParamList = this.GetRoleRightSaveParamList(rightForDel, DataSaveOption.Delete);
                    dbHelper.ExecuteNonQuery(roleRightParamList, transaction, con, "uspSetRightsInRoles");
                    int roleRightId = dbHelper.IdentityValue;

                    if (rightList != null)
                    {
                        foreach (Right right in rightList)
                        {
                            if (right.IsAssignedRight)
                            {
                                right.RoleId = roleId;
                                roleRightParamList = this.GetRoleRightSaveParamList(right, DataSaveOption.Insert);
                                dbHelper.ExecuteNonQuery(roleRightParamList, transaction, con, "uspSetRightsInRoles");
                                roleRightId = dbHelper.IdentityValue;
                            }
                        }
                    }

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

            return roleId;

        }


        public override List<Role> LoadItemList(Role item, DataLoadOption lOption)
        {

            List<Role> roleList = new List<Role>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetRole");

                foreach (DataRow dr in dt.Rows)
                {
                    Role roleInfo = new Role();
                    MapperBase.GetInstance().MapItem(roleInfo, dr);
                    roleList.Add(roleInfo);
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

            return roleList;

        }

        public override List<Role> LoadItemList(Role item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Role LoadItem(Role item, DataLoadOption lOption)
        {
            CustomParameterList paramList = this.GetLoadParamList(item, lOption);
            DBHelper dbHelper = DBHelper.GetInstance();
            DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetRole");

            Role roleInfo = new Role();
            if (dt.Rows.Count > 0)
            {
                MapperBase.GetInstance().MapItem(roleInfo, (DataRow)dt.Rows[0]);
            }

            return roleInfo;
        }

        private CustomParameterList GetLoadParamList(Role item, DataLoadOption lOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            paramList.Add(new CustomParameter("@RoleId", item.RoleId, DbType.Int32));
            paramList.Add(new CustomParameter("@UserId", item.UserId, DbType.Int32));
            paramList.Add(new CustomParameter("@ModuleId", item.ModuleId, DbType.Int32));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.Int32));
            paramList.Add(new CustomParameter("@UserGroupId", item.UserGroupId, DbType.Int32));


            return paramList;
        }


        private CustomParameterList GetSaveParamList(Role item, DataSaveOption sOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@SaveOption", sOption, DbType.Int32));
            paramList.Add(new CustomParameter("@RoleId", item.RoleId, DbType.Int32));
            paramList.Add(new CustomParameter("@RoleName", item.RoleName, DbType.String));
            paramList.Add(new CustomParameter("@ModuleId", item.ModuleId, DbType.Int32));
            paramList.Add(new CustomParameter("@Description", item.Description, DbType.String));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.Int32));


            return paramList;

        }


        private CustomParameterList GetRoleMenuSaveParamList(Menu item, DataSaveOption sOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@SaveOption", sOption, DbType.Int32));
            paramList.Add(new CustomParameter("@RoleId", item.RoleId, DbType.Int32));
            paramList.Add(new CustomParameter("@MenuId", item.MenuId, DbType.Int32));           
            paramList.Add(new CustomParameter("@IsAddAssigned", item.IsAddAssigned, DbType.Boolean));
            paramList.Add(new CustomParameter("@IsEditAssigned", item.IsEditAssigned, DbType.Boolean));
            paramList.Add(new CustomParameter("@IsDeleteAssigned", item.IsDeleteAssigned, DbType.Boolean));
            paramList.Add(new CustomParameter("@IsCancelAssigned", item.IsCancelAssigned, DbType.Boolean));
            paramList.Add(new CustomParameter("@IsPrintAssigned", item.IsPrintAssigned, DbType.Boolean));


            return paramList;

        }


        private CustomParameterList GetRoleRightSaveParamList(Right item, DataSaveOption sOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@SaveOption", sOption, DbType.Int32));
            paramList.Add(new CustomParameter("@RoleId", item.RoleId, DbType.Int32));
            paramList.Add(new CustomParameter("@RightId", item.RightId, DbType.Int32));

            return paramList;

        }


        public static RoleDAL GetInstance()
        {
            return new RoleDAL();
        }
    }
}
