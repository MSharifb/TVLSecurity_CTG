using System;
using System.Collections.Generic;
using Entity;
using System.Data;
using PrimaryBaseLibrary;
using DBClient;

namespace DataAccess
{
   public class ApplicationDAL : DataAccessBase<Application>
    {

        public override int Save(Application item, DataSaveOption sOption)
        {
            DBHelper dbHelper = DBHelper.GetInstance();
            int id = 0;
            try
            {
                CustomParameterList paramList = this.GetSaveParamList(item, sOption);
                
                dbHelper.ExecuteNonQuery(paramList, "uspSetApplication");
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

        public override List<Application> LoadItemList(Application item, DataLoadOption lOption)
        {
            List<Application> appList = new List<Application>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetApplication");

                foreach (DataRow dr in dt.Rows)
                {
                    Application appInfo = new Application();
                    MapperBase.GetInstance().MapItem(appInfo, dr);
                    appList.Add(appInfo);
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

            return appList;
        }

        public override List<Application> LoadItemList(Application item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Application LoadItem(Application item, DataLoadOption lOption)
        {
            Application appInfo = new Application();
            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetApplication");

                if (dt.Rows.Count > 0)
                {
                    MapperBase.GetInstance().MapItem(appInfo, dt.Rows[0]);                    
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

            return appInfo;
        }

        private CustomParameterList GetLoadParamList(Application item, DataLoadOption lOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.Int32));
            paramList.Add(new CustomParameter("@ApplicationName", item.ApplicationName, DbType.String));
            paramList.Add(new CustomParameter("@ApplicationTitle", item.ApplicationTitle, DbType.Int32));
            paramList.Add(new CustomParameter("@Description", item.Description, DbType.String));


            return paramList;
        }


        private CustomParameterList GetSaveParamList(Application item, DataSaveOption sOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@SaveOption", sOption, DbType.Int32));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.Int32));
            paramList.Add(new CustomParameter("@ApplicationName", item.ApplicationName, DbType.String));
            paramList.Add(new CustomParameter("@ApplicationTitle", item.ApplicationTitle, DbType.Int32));
            paramList.Add(new CustomParameter("@Description", item.Description, DbType.String));
            paramList.Add(new CustomParameter("@IsVerificationEnable", item.IsVerificationEnable, DbType.Boolean));

            return paramList;

        }

        public static ApplicationDAL GetInstance()
        {
            return new ApplicationDAL();
        }

    }
}
