using System;
using System.Collections.Generic;
using Entity;
using System.Data;
using PrimaryBaseLibrary;
using DBClient;

namespace DataAccess
{
    public class DepartmentDAL : DataAccessBase<Department>
    {
        public override int Save(Department item, DataSaveOption sOption)
        {
            throw new NotImplementedException();
        }

        public override List<Department> LoadItemList(Department item, DataLoadOption lOption)
        {
            List<Department> departmentList = new List<Department>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetDepartment");

                foreach (DataRow dr in dt.Rows)
                {
                    Department department = new Department();
                    MapperBase.GetInstance().MapItem(department, dr);
                    departmentList.Add(department);
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

            return departmentList;
        }

        public override List<Department> LoadItemList(Department item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Department LoadItem(Department item, DataLoadOption lOption)
        {
            throw new NotImplementedException();
        }

        private CustomParameterList GetLoadParamList(Department item, DataLoadOption lOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));                           
            paramList.Add(new CustomParameter("@strDepartmentID", item.strDepartmentID, DbType.String));
            paramList.Add(new CustomParameter("@strDepartment", item.strDepartment, DbType.String));

            return paramList;
        }

        public static DepartmentDAL GetInstance()
        {
            return new DepartmentDAL();
        }      

    }
}
