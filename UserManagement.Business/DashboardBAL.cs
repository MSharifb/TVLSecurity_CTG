using System;
using Entity;
using DataAccess;
using System.Collections.Generic;
using PrimaryBaseLibrary;

namespace UserManagement.Business
{
    public class DashboardBAL : BusinessBase<Dashboard>
    {
        public static DashboardBAL GetInstance()
        {
            return new DashboardBAL();
        }

        #region CRUD
        public override int InsertData(Dashboard item)
        {
            throw new NotImplementedException();
        }

        public override int UpdateData(Dashboard item)
        {
            throw new NotImplementedException();
        }

        public override int DeleteData(int Id)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Load
        public override List<Dashboard> LoadItemList(Dashboard item)
        {
            return DashboardDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadByCraiteria);
        }

        public override List<Dashboard> LoadItemList(Dashboard item, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Dashboard LoadItem(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Dashboard> LoadAllItemList()
        {
             return DashboardDAL.GetInstance().LoadItemList(new Dashboard(), DataLoadOption.LoadAll);          
            
        }
        #endregion
        
    }
}
