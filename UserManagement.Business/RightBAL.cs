using System;
using Entity;
using DataAccess;
using System.Collections.Generic;
using PrimaryBaseLibrary;

namespace UserManagement.Business
{
   public class RightBAL : BusinessBase<Right>
    {
        public override int InsertData(Right item)
        {
            throw new NotImplementedException();
        }

        public override int UpdateData(Right item)
        {
            throw new NotImplementedException();
        }

        public override int DeleteData(int Id)
        {
            throw new NotImplementedException();
        }

        public override List<Right> LoadAllItemList()
        {
            return RightDAL.GetInstance().LoadItemList(new Right(), DataLoadOption.LoadAll);
        }

        public override List<Right> LoadItemList(Right item)
        {
            return RightDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadByCraiteria);
        }

        public  List<Right> LoadRightsMapedByRole(int roleId)
        {
            Right item = new Right();
            item.RoleId = roleId;
            return RightDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadByCraiteria);
        }

        public override List<Right> LoadItemList(Right item, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Right LoadItem(int id)
        {
            throw new NotImplementedException();
        }

        public Right LoadRightByLoginIdAndRightName(string loginId,string rightName)
        {
            Right item = new Right();
            item.RightName = rightName;
            item.LoginId = loginId;
            return RightDAL.GetInstance().LoadItem(item, DataLoadOption.LoadByCraiteria);
        }

        public static RightBAL GetInstance()
        {
            return new RightBAL();
        }
    }
}
