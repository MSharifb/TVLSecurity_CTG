using System;
using System.Collections.Generic;
using Entity;
using System.Data;
using DBClient;
using PrimaryBaseLibrary;

namespace DataAccess
{
   public class CustomSessionDAL : DataAccessBase<Entity.CustomSession>
    {

        public override int Save(CustomSession item, DataSaveOption sOption)
        {
            int id = 0;
            try
            {
                CustomParameterList paramList = this.GetSaveParamList(item, sOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                dbHelper.ExecuteNonQuery(paramList, "[uspSetSessions]");
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

        public override List<CustomSession> LoadItemList(CustomSession item, DataLoadOption lOption)
        {
            List<CustomSession> sessionList = new List<CustomSession>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetSessions");

                foreach (DataRow dr in dt.Rows)
                {
                    CustomSession cSession = new CustomSession();
                    MapperBase.GetInstance().MapItem(cSession, dr);
                    sessionList.Add(cSession);
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

            return sessionList;
  
        }

        public override List<CustomSession> LoadItemList(CustomSession item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override CustomSession LoadItem(CustomSession item, DataLoadOption lOption)
        {
            CustomParameterList paramList = this.GetLoadParamList(item, lOption);
            DBHelper dbHelper = DBHelper.GetInstance();
            DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetSessions");

            CustomSession cSession = new CustomSession();
            if (dt.Rows.Count > 0)
            {
                MapperBase.GetInstance().MapItem(cSession, (DataRow)dt.Rows[0]);
            }

            return cSession;
        }


        private CustomParameterList GetLoadParamList(CustomSession item, DataLoadOption lOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            paramList.Add(new CustomParameter("@SESSIONID",item.SESSIONID,DbType.String));
            paramList.Add(new CustomParameter("@ApplicationName", item.ApplicationName, DbType.Int32));
            return paramList;
        }


        private CustomParameterList GetSaveParamList(CustomSession item, DataSaveOption sOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@SaveOption", sOption, DbType.Int32));
            paramList.Add(new CustomParameter("SESSIONID", item.SESSIONID, DbType.String));
            paramList.Add(new CustomParameter("@ApplicationName", item.ApplicationName, DbType.String));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.Int32));
            paramList.Add(new CustomParameter("@CREATED", item.CREATED, DbType.DateTime));
            paramList.Add(new CustomParameter("@EXPIRES", item.EXPIRES, DbType.DateTime));
            paramList.Add(new CustomParameter("@LOCKDATE", item.LOCKDATE, DbType.DateTime));
            paramList.Add(new CustomParameter("@LOCKID", item.LOCKID, DbType.Int32));
            paramList.Add(new CustomParameter("@TIMEOUT", item.TIMEOUT, DbType.Int32));
            paramList.Add(new CustomParameter("@LOCKED", item.LOCKED, DbType.Int32));
            paramList.Add(new CustomParameter("@SESSIONITEMS", item.SESSIONITEMS, DbType.String));
            paramList.Add(new CustomParameter("@FLAGS", item.FLAGS, DbType.String));
            paramList.Add(new CustomParameter("@StrMode", item.OperationMode, DbType.String));
           

            return paramList;

        }

        public static CustomSessionDAL GetInstance()
        {
            return new CustomSessionDAL();
        }      
    }
}
