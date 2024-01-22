using System;
using Entity;
using DataAccess;
using System.Collections.Generic;
using PrimaryBaseLibrary;

namespace UserManagement.Business
{
   public class EmployeeBAL:BusinessBase<Employee>
    {

        public override int InsertData(Employee item)
        {
            throw new NotImplementedException();
        }

        public override int UpdateData(Employee item)
        {
            throw new NotImplementedException();
        }

        public override int DeleteData(int Id)
        {
            throw new NotImplementedException();
        }

        public override List<Employee> LoadAllItemList()
        {
            return EmployeeDAL.GetInstance().LoadItemList(new Employee(), DataLoadOption.LoadAll);
        }

        public override List<Employee> LoadItemList(Employee item)
        {
            return EmployeeDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadByCraiteria);
        }

        public override List<Employee> LoadItemList(Employee item, int startRowIndex, int maxRow)
        {
           return  EmployeeDAL.GetInstance().LoadItemList(item,DataLoadOption.LoadByCraiteria, startRowIndex, maxRow);
        }

        public List<Employee> LoadSearchItemList(Employee item, int startRowIndex, int maxRow, out int intTotalRowCount)
        {
            return EmployeeDAL.GetInstance().LoadSearchItemList(item, DataLoadOption.LoadByCraiteria, startRowIndex, maxRow, out intTotalRowCount);
        }

        public Int32 GetTotalEmploeeCount(Employee item)
        {
            return EmployeeDAL.GetInstance().GetItemCount(item, DataLoadOption.LoadForTotalCount);
        }
        
        public override Employee LoadItem(int id)
        {
            throw new NotImplementedException();
        }

        public static EmployeeBAL GetInstance()
        {
            return new EmployeeBAL();
        }
    }
}
