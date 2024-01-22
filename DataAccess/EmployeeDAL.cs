using System;
using System.Collections.Generic;
using Entity;
using System.Data;
using PrimaryBaseLibrary;
using DBClient;

namespace DataAccess
{
   public class EmployeeDAL : DataAccessBase<Employee>
    {
        public override int Save(Employee item, DataSaveOption sOption)
        {
            throw new NotImplementedException();
        }

        public override List<Employee> LoadItemList(Employee item, DataLoadOption lOption)
        {
            List<Employee> empList = new List<Employee>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item,lOption,0,0);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetEmployee");

                foreach (DataRow dr in dt.Rows)
                {
                    Employee employee = new Employee();
                    MapperBase.GetInstance().MapItem(employee, dr);
                    empList.Add(employee);
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

            return empList;
        }

        public override List<Employee> LoadItemList(Employee item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
          
            List<Employee> empList = new List<Employee>();

            try
            {
               
                DBHelper dbHelper = DBHelper.GetInstance();

                CustomParameterList paramList = this.GetLoadParamList(item, lOption, startRowIndex, maxRow);

                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList,"uspGetEmployee");
                
                foreach (DataRow dr in dt.Rows)
                {
                    Employee employee = new Employee();
                    MapperBase.GetInstance().MapItem(employee, dr);
                    empList.Add(employee);
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

            return empList;
        }

        public List<Employee> LoadSearchItemList(Employee item, DataLoadOption lOption, int startRowIndex, int maxRow, out int intTotalRowCount)
        {

            List<Employee> empList = new List<Employee>();

            try
            {
                object paramval = null;
                DBHelper dbHelper = DBHelper.GetInstance();

                CustomParameterList paramList = this.GetLoadParamList(item,lOption, startRowIndex, maxRow);
                
               
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList,ref  paramval, "uspGetEmployee");
                intTotalRowCount = (int)paramval;

                foreach (DataRow dr in dt.Rows)
                {
                    Employee employee = new Employee();
                    MapperBase.GetInstance().MapItem(employee, dr);
                    empList.Add(employee);
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

            return empList;
        }

        public Int32 GetItemCount(Employee item, DataLoadOption lOption)
        {
            Int32 totalRow = 0;

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item,lOption,0,0);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetEmployee");

             
               totalRow = Convert.ToInt32(dt.Rows[0][0]);
               
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

            return totalRow;
        }

        public override Employee LoadItem(Employee item, DataLoadOption lOption)
        {
            throw new NotImplementedException();
        }
        private CustomParameterList GetLoadParamList(Employee item, DataLoadOption lOption, int startRowIndex, int maxRows)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            paramList.Add(new CustomParameter("@StartRowIndex", startRowIndex, DbType.Int32));
            paramList.Add(new CustomParameter("@MaxRows", maxRows, DbType.Int32));
            paramList.Add(new CustomParameter("@strEmpID", item.EmpId, DbType.String));
            paramList.Add(new CustomParameter("@strEmpName", item.EmpName, DbType.String));
            paramList.Add(new CustomParameter("@strDepartmentID", item.strDepartmentID, DbType.String));
            paramList.Add(new CustomParameter("@strDesignationID", item.strDesignationID, DbType.String));
            paramList.Add(new CustomParameter("@ZoneId", item.ZoneId.ToString(), DbType.String));
            paramList.Add(new CustomParameter("@numTotalRows", 0, DbType.String, ParameterDirection.Output));
            return paramList;
            //
        }

        public static EmployeeDAL GetInstance()
        {
            return new EmployeeDAL();
        }     
    }
}
