using System;
using System.Collections.Generic;
using Entity;
using System.Data;
using PrimaryBaseLibrary;
using DBClient;

namespace DataAccess
{
    public class UserRoleDAL : DataAccessBase<UserRole>
    {
        public override int Save(UserRole item, DataSaveOption sOption)
        {
            int resutl = 0;
            try
            {
                CustomParameterList paramList = this.GetSaveParamList(item, sOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                dbHelper.ExecuteNonQuery(paramList, "uspSetUsersRoles");
                resutl = dbHelper.IdentityValue;

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


        public int SaveUserRolesWithTran(int userId, List<Role> roleList,int applicationId,int moduleId, DataSaveOption sOption)
        {
            IDbTransaction transaction = null;
            int id = 0;

            try
            {

                IDbConnection con = DBHelper.GetConnection();
                con.Open();
                transaction = DBHelper.GetTransaction(con);

                try
                {
                    UserRole userDelRole = new UserRole();
                    userDelRole.UserId = userId;
                    userDelRole.ApplicationId = applicationId;
                    userDelRole.ModuleId = moduleId;

                    CustomParameterList paramListForDel = this.GetSaveParamList(userDelRole, DataSaveOption.Delete);
                    DBHelper dbHelper = DBHelper.GetInstance();
                    dbHelper.ExecuteNonQuery(paramListForDel, transaction, con, "uspSetUsersRoles");

                    List<UserRole> userRoleList = new List<UserRole>();

                    foreach (Role role in roleList)
                    {
                        if (role.IsAssignedRole == true)
                        {
                            UserRole userRole = new UserRole();
                            userRole.UserId = userId;
                            userRole.RoleId = role.RoleId;
                            paramListForDel = this.GetSaveParamList(userRole, DataSaveOption.Insert);
                            dbHelper.ExecuteNonQuery(paramListForDel, transaction, con, "uspSetUsersRoles");

                        }
                    }


                    transaction.Commit();


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
                if (transaction != null)
                {
                    transaction.Rollback();

                }
                throw ce;

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                CaughtException ce = new CaughtException(ex.Message, this, "Save");
                throw ce;
            }

            return id;

        }

        //public UserRoleInfo LoadUserRole(UserRoleInfo item, DataLoadOption lOption)
        //{
        //    CustomParameterList paramList = this.GetLoadParamList(item, lOption);
        //    DBHelper dbHelper = DBHelper.GetInstance();
        //    DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetUserRole");

        //    UserRoleInfo userRoleInfo = new UserRoleInfo();
        //    if (dt.Rows.Count > 0)
        //    {
        //        MapperBase.GetInstance().MapItem(userRoleInfo, (DataRow)dt.Rows[0]);
        //    }

        //    return userRoleInfo;
        //}

        public List<UserRoleInfo> LoadUserRole (UserRoleInfo item, DataLoadOption lOption)
        {

            List<UserRoleInfo> userRoleList = new List<UserRoleInfo>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetUserRole");

                foreach (DataRow dr in dt.Rows)
                {
                    UserRoleInfo userRoleInfo = new UserRoleInfo();
                    MapperBase.GetInstance().MapItem(userRoleInfo, dr);
                    userRoleList.Add(userRoleInfo);
                }
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                CaughtException ce = new CaughtException(ex.Message, this, "LoadUserRole");
                throw ce;
            }

            return userRoleList;

        }
        private CustomParameterList GetLoadParamList(UserRoleInfo item, DataLoadOption lOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            paramList.Add(new CustomParameter("@UserRoleId", item.UserRoleId, DbType.Int32));
            paramList.Add(new CustomParameter("@UserId", item.UserId, DbType.Int32));
            paramList.Add(new CustomParameter("@RoleId", item.RoleId, DbType.String));
            
            return paramList;
        }

        public override List<UserRole> LoadItemList(UserRole item, DataLoadOption lOption)
        {
            throw new NotImplementedException();
        }

        public override List<UserRole> LoadItemList(UserRole item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override UserRole LoadItem(UserRole item, DataLoadOption lOption)
        {
            throw new NotImplementedException();
        }

        private CustomParameterList GetSaveParamList(UserRole item, DataSaveOption sOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@SaveOption", sOption, DbType.Int32));
            paramList.Add(new CustomParameter("@UserRoleId", item.UserRoleId, DbType.Int32));
            paramList.Add(new CustomParameter("@UserId", item.UserId, DbType.Int32));
            paramList.Add(new CustomParameter("@RoleId", item.RoleId, DbType.Int32));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.Int32));
            paramList.Add(new CustomParameter("@ModuleId", item.ModuleId, DbType.Int32));
           


            return paramList;

        }

        public static UserRoleDAL GetInstance()
        {
            return new UserRoleDAL();
        }

    }
}
