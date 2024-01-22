using System;
using System.Collections.Generic;
using Entity;
using DataAccess;
using PrimaryBaseLibrary;

namespace UserManagement.Business
{
  public  class RoleGroupBAL : BusinessBase<RoleGroup>
    {
        public override int InsertData(RoleGroup item)
        {
            return RoleGroupDAL.GetInstance().Save(item, DataSaveOption.Insert);
        }

        public override int UpdateData(RoleGroup item)
        {
            return RoleGroupDAL.GetInstance().Save(item,DataSaveOption.Update);
        }

        public override int DeleteData(int Id)
        {
            RoleGroup rgroup  = new RoleGroup();
            rgroup.RoleGroupsId = Id;
            return RoleGroupDAL.GetInstance().Save(rgroup, DataSaveOption.Delete);
        }

        public override List<RoleGroup> LoadAllItemList()
        {
          return  RoleGroupDAL.GetInstance().LoadItemList(new RoleGroup(), DataLoadOption.LoadAll);
        }

        public override List<RoleGroup> LoadItemList(RoleGroup item)
        {
            throw new NotImplementedException();
        }

        public override List<RoleGroup> LoadItemList(RoleGroup item, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override RoleGroup LoadItem(int id)
        {
            RoleGroup rgroup = new RoleGroup();
            rgroup.RoleGroupsId = id;

            return RoleGroupDAL.GetInstance().LoadItem(rgroup, DataLoadOption.LoadByCraiteria);
        }

        public static RoleGroupBAL GetInstance()
        {
            return new RoleGroupBAL();
        }
    }
}
