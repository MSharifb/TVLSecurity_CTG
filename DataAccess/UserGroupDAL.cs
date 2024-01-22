using System;
using System.Collections.Generic;
using Entity;
using System.Data.SqlClient;
using System.Data;
using DBClient;
using PrimaryBaseLibrary;

namespace DataAccess
{
   public class UserGroupDAL:DataAccessBase<UserGroup>
    {
        public override int Save(UserGroup item, DataSaveOption sOption)
        {
            int id = 0;
            try
            {
                CustomParameterList paramList = this.GetSaveParamList(item, sOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                dbHelper.ExecuteNonQuery(paramList, "uspSetGroup");
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


        public int Save(UserGroup item,List<Role> roleList, DataSaveOption sOption)
        {
            int id = 0;
            IDbConnection con = DBHelper.GetConnection();
            con.Open();
            IDbTransaction transaction = DBHelper.GetTransaction(con);

            try
            {
             

                CustomParameterList paramList = this.GetSaveParamList(item, sOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                dbHelper.ExecuteNonQuery(paramList,transaction,con,"uspSetGroup");
                id = dbHelper.IdentityValue;

                UserGroupRole ugDelRole = new UserGroupRole();
                ugDelRole.GroupId = id;
                ugDelRole.ApplicationId = item.ApplicationId;
                ugDelRole.ModuleId = item.ModuleId;
                CustomParameterList groupRoleParamList = this.GetGroupRoleSaveParamList(ugDelRole, DataSaveOption.Delete);
                dbHelper.ExecuteNonQuery(groupRoleParamList, transaction, con, "uspSetGroupRole");

                foreach (Role role in roleList)
                {
                    if (role.IsAssignedRole)
                    {
                        UserGroupRole ugRole = new UserGroupRole();
                        ugRole.GroupId = id;
                        ugRole.RoleId = role.RoleId;
                        groupRoleParamList = this.GetGroupRoleSaveParamList(ugRole, DataSaveOption.Insert);
                        ugRole.GroupRoleId = dbHelper.ExecuteNonQuery(groupRoleParamList, transaction, con, "uspSetGroupRole");
                        
                    }
                }

                transaction.Commit();
                con.Close();
                
            }
            catch (CaughtException ce)
            {
                transaction.Rollback();
                return ce.ErrorCode * -1;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                CaughtException ce = new CaughtException(ex.Message, this, "Save");
                throw ce;
            }

            return id;
        }

        public override List<UserGroup> LoadItemList(UserGroup item, DataLoadOption lOption)
        {
            List<UserGroup> gList = new List<UserGroup>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetGroup");

                foreach (DataRow dr in dt.Rows)
                {
                    UserGroup gInfo = new UserGroup();
                    MapperBase.GetInstance().MapItem(gInfo, dr);
                    gList.Add(gInfo);
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

            return gList;
        }

        public override List<UserGroup> LoadItemList(UserGroup item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override UserGroup LoadItem(UserGroup item, DataLoadOption lOption)
        {
            UserGroup gInfo = new UserGroup();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetGroup");

                if(dt.Rows.Count > 0)
                {
                   
                    MapperBase.GetInstance().MapItem(gInfo, dt.Rows[0]);                    
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

            return gInfo;
        }


        private CustomParameterList GetLoadParamList(UserGroup item, DataLoadOption lOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            paramList.Add(new CustomParameter("@GroupId", item.GroupId, DbType.Int32));
            return paramList;
        }




        private CustomParameterList GetSaveParamList(UserGroup item, DataSaveOption sOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@SaveOption", sOption, DbType.Int32));
            paramList.Add(new CustomParameter("@GroupId", item.GroupId, DbType.Int32));
            paramList.Add(new CustomParameter("@GroupName", item.GroupName, DbType.String));
            paramList.Add(new CustomParameter("@Description", item.Description, DbType.String));
            //paramList.Add(new CustomParameter("ApplicationId", item.ApplicationId, DbType.Int32));
            return paramList;
        }


        private CustomParameterList GetGroupRoleSaveParamList(UserGroupRole item, DataSaveOption sOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@SaveOption", sOption, DbType.Int32));
            paramList.Add(new CustomParameter("@GroupId", item.GroupId, DbType.Int32));
            paramList.Add(new CustomParameter("@RoleId", item.RoleId, DbType.Int32));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.Int32));
            paramList.Add(new CustomParameter("@ModuleId", item.ModuleId, DbType.Int32));
          
            return paramList;
        }
        public static UserGroupDAL GetInstance()
        {
            return new UserGroupDAL();
        }       
    }
}
