using System;
using DataAccess;
using System.Collections.Generic;
using PrimaryBaseLibrary;
using Entity;

namespace UserManagement.Business
{
    public class DepartmentBAL : BusinessBase<Department>
    {
        public override int InsertData(Department item)
        {
            throw new NotImplementedException();
        }

        public override int UpdateData(Department item)
        {
            throw new NotImplementedException();
        }

        public override int DeleteData(int Id)
        {
            throw new NotImplementedException();
        }

        public override List<Department> LoadAllItemList()
        {
            return DepartmentDAL.GetInstance().LoadItemList(new Department(), DataLoadOption.LoadAll);
        }

        public override List<Department> LoadItemList(Department item)
        {
            return DepartmentDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadByCraiteria);
        }

        public override List<Department> LoadItemList(Department item, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Department LoadItem(int id)
        {
            throw new NotImplementedException();
        }

        public static DepartmentBAL GetInstance()
        {
            return new DepartmentBAL();
        }

    }
}
