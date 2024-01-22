using DBClient;
using Entity;
using PrimaryBaseLibrary;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class DashboardDAL : DataAccessBase<Dashboard>
    {
        public override int Save(Dashboard item, DataSaveOption sOption)
        {
            throw new NotImplementedException();
        }

        public override List<Dashboard> LoadItemList(Dashboard item, DataLoadOption lOption)
        {
            List<Dashboard> dashboadList = new List<Dashboard>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetZoneWiseUserList");

                foreach (DataRow dr in dt.Rows)
                {
                    Dashboard obj = new Dashboard();
                    MapperBase.GetInstance().MapItem(obj, dr);
                    dashboadList.Add(obj);
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

            return dashboadList;
        }

        public override List<Dashboard> LoadItemList(Dashboard item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Dashboard LoadItem(Dashboard item, DataLoadOption lOption)
        {
            Dashboard dashboadList = new Dashboard();
            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetZoneWiseUserList");

                if (dt.Rows.Count > 0)
                {
                    MapperBase.GetInstance().MapItem(dashboadList, dt.Rows[0]);
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

            return dashboadList;
        }
       

        private CustomParameterList GetLoadParamList(Dashboard item, DataLoadOption lOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            
            return paramList;
        }

        public static DashboardDAL GetInstance()
        {
            return new DashboardDAL();
        }
    }
}
