using System;
using System.Collections.Generic;
using Entity;
using System.Data;
using PrimaryBaseLibrary;
using DBClient;

namespace DataAccess
{
    public class RightDAL : DataAccessBase<Right>
    {
        public override int Save(Right item, DataSaveOption sOption)
        {
            throw new NotImplementedException();
        }

        public override List<Right> LoadItemList(Right item, DataLoadOption lOption)
        {
            List<Right> rightList = new List<Right>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetRight");

                foreach (DataRow dr in dt.Rows)
                {
                    Right right = new Right();
                    MapperBase.GetInstance().MapItem(right, dr);
                    rightList.Add(right);
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

            return rightList;

        }

        public override List<Right> LoadItemList(Right item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Right LoadItem(Right item, DataLoadOption lOption)
        {
            Right right = new Right();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetRight");
                MapperBase.GetInstance().MapItem(right, dt.Rows[0]);


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

            return right;
        }


        private CustomParameterList GetLoadParamList(Right item, DataLoadOption lOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            paramList.Add(new CustomParameter("@RoleId", item.RoleId, DbType.Int32));
            paramList.Add(new CustomParameter("@RightId", item.RightId, DbType.Int32));
            paramList.Add(new CustomParameter("@RightName", item.RightName, DbType.String));
            paramList.Add(new CustomParameter("@LoginId", item.LoginId, DbType.String));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.String));
            paramList.Add(new CustomParameter("@ModuleId", item.ModuleId, DbType.Int32));
            return paramList;
        }

        public static RightDAL GetInstance()
        {
            return new RightDAL();
        }
    }
}
