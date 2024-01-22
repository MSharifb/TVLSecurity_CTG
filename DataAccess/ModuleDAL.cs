using System;
using System.Collections.Generic;
using Entity;
using System.Data;
using PrimaryBaseLibrary;
using DBClient;

namespace DataAccess
{
    public class ModuleDAL : DataAccessBase<Module>
    {

        public override int Save(Module item, DataSaveOption sOption)
        {
            int id = 0;
            DBHelper dbHelper = DBHelper.GetInstance();

            try
            {
                CustomParameterList paramList = this.GetSaveParamList(item, sOption);                
                dbHelper.ExecuteNonQuery(paramList, "uspSetModule");
                id = dbHelper.IdentityValue;

            }
            catch (CaughtException ce)
            {
                return dbHelper.ErrorCode * -1;
            }
            catch (Exception ex)
            {
                CaughtException ce = new CaughtException(ex.Message, this, "Save");
                throw ce;
            }

            return id;
        }

        public override List<Module> LoadItemList(Module item, DataLoadOption lOption)
        {
            List<Module> moduleList = new List<Module>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetModule");

                foreach (DataRow dr in dt.Rows)
                {
                    Module moduleInfo = new Module();
                    MapperBase.GetInstance().MapItem(moduleInfo, dr);
                    moduleList.Add(moduleInfo);
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

            return moduleList;
        }

        public override List<Module> LoadItemList(Module item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Module LoadItem(Module item, DataLoadOption lOption)
        {
            Module moduleInfo = new Module();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetModule");

                MapperBase.GetInstance().MapItem(moduleInfo, dt.Rows[0]);

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

            return moduleInfo;
        }


        private CustomParameterList GetSaveParamList(Module item, DataSaveOption sOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@SaveOption", sOption, DbType.Int32));
            paramList.Add(new CustomParameter("@ModuleId", item.ModuleId, DbType.Int32));
            paramList.Add(new CustomParameter("@ModuleTitle", item.ModuleTitle, DbType.String));
            paramList.Add(new CustomParameter("@ModuleName", item.ModuleName, DbType.String));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.Int32));
            paramList.Add(new CustomParameter("@Description", item.Description, DbType.String));
            paramList.Add(new CustomParameter("@SortOrder", item.SortOrder, DbType.Int32));
            return paramList;

        }


        private CustomParameterList GetLoadParamList(Module item, DataLoadOption lOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            paramList.Add(new CustomParameter("@ModuleId", item.ModuleId, DbType.Int32));
            paramList.Add(new CustomParameter("@ModuleTitle", item.ModuleTitle, DbType.String));
            paramList.Add(new CustomParameter("@ModuleName", item.ModuleName, DbType.String));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.Int32));
            paramList.Add(new CustomParameter("@Description", item.Description, DbType.String));
            paramList.Add(new CustomParameter("@SortOrder", item.SortOrder, DbType.Int32));
            return paramList;
        }

        public static ModuleDAL GetInstance()
        {
            return new ModuleDAL();
        }

    }
}
