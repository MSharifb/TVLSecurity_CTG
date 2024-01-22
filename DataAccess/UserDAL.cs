using System;
using System.Collections.Generic;
using Entity;
using System.Data;
using PrimaryBaseLibrary;
using DBClient;

namespace DataAccess
{
    public class UserDAL : DataAccessBase<User>
    {

        public override System.Int32 Save(User item, DataSaveOption sOption)
        {
            int id = 0;
            try
            {
                CustomParameterList paramList = this.GetSaveParamList(item, sOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                dbHelper.ExecuteNonQuery(paramList, "uspSetUser");
                id = dbHelper.IdentityValue;

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

            return id;

        }

        public override List<User> LoadItemList(User item, DataLoadOption lOption)
        {

            List<User> uList = new List<User>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetUser");

                foreach (DataRow dr in dt.Rows)
                {
                    User uInfo = new User();
                    MapperBase.GetInstance().MapItem(uInfo, dr);
                    uList.Add(uInfo);
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

            return uList;

        }

        public override List<User> LoadItemList(User item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override User LoadItem(User item, DataLoadOption lOption)
        {
            CustomParameterList paramList = this.GetLoadParamList(item, lOption);
            DBHelper dbHelper = DBHelper.GetInstance();
            DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetUser");

            User uInfo = new User();
            if (dt.Rows.Count > 0)
            {
                MapperBase.GetInstance().MapItem(uInfo, (DataRow)dt.Rows[0]);
            }

            return uInfo;
        }

        private CustomParameterList GetLoadParamList(User item, DataLoadOption lOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            paramList.Add(new CustomParameter("@UserId", item.UserId, DbType.Int32));                    
            paramList.Add(new CustomParameter("@LoginId", item.LoginId, DbType.String));
            paramList.Add(new CustomParameter("@FirstName", item.FirstName, DbType.String));
            paramList.Add(new CustomParameter("@LastName", item.LastName, DbType.String));
            paramList.Add(new CustomParameter("@Password", item.Password, DbType.String));
            paramList.Add(new CustomParameter("@ZoneId", item.ZoneId, DbType.Int32));

            return paramList;
        }


        private CustomParameterList GetSaveParamList(User item, DataSaveOption sOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@SaveOption", sOption, DbType.Int32));
            paramList.Add(new CustomParameter("@UserId", item.UserId, DbType.Int32));
            paramList.Add(new CustomParameter("@FirstName", item.FirstName, DbType.String));
            paramList.Add(new CustomParameter("@LastName", item.LastName, DbType.String));

            if (item.LoginId != null && item.LoginId.Length > 0)
            {
                paramList.Add(new CustomParameter("@LoginId", item.LoginId.ToString().Trim(), DbType.String));
            }
            else
            {
                paramList.Add(new CustomParameter("@LoginId", item.LoginId, DbType.String));
            }

            if (item.EmailAddress != null && item.EmailAddress.Length > 0)
            {
                paramList.Add(new CustomParameter("@EmailAddress", item.EmailAddress.Trim(), DbType.String));
            }
            else
            {
                paramList.Add(new CustomParameter("@EmailAddress", item.EmailAddress, DbType.String));
            }
            paramList.Add(new CustomParameter("@Phone", item.Phone, DbType.String));
            paramList.Add(new CustomParameter("@Password", item.Password, DbType.String));
            paramList.Add(new CustomParameter("@NeverExperied", item.NeverExperied, DbType.Boolean));
            paramList.Add(new CustomParameter("@CreatedBy", item.CreatedBy, DbType.String));
            paramList.Add(new CustomParameter("@UpdatedBy", item.UpdatedBy, DbType.String));
            paramList.Add(new CustomParameter("@LastLoginDate", item.LastLoginDate, DbType.DateTime));
            paramList.Add(new CustomParameter("@GroupId", item.GroupId, DbType.Int32));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.Int32));
            paramList.Add(new CustomParameter("@EmpId", item.EmpId, DbType.String));
            paramList.Add(new CustomParameter("@ChangePasswordAtFirstLogin", item.ChangePasswordAtFirstLogin, DbType.Boolean));
            paramList.Add(new CustomParameter("@Status", item.Status, DbType.Boolean));
            paramList.Add(new CustomParameter("@ZoneId", item.ZoneId, DbType.Int32));
            return paramList;
        }

        public static UserDAL GetInstance()
        {
            return new UserDAL();
        }
    }
}
