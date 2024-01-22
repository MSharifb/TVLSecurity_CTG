using System;
using Entity;
using DataAccess;
using System.Collections.Generic;
using PrimaryBaseLibrary;

namespace TVLSecurity.Business
{
   public class CustomSessionBAL : BusinessBase<CustomSession>
    {
        public override int InsertData(CustomSession item)
        {
            return CustomSessionDAL.GetInstance().Save(item, DataSaveOption.Insert);
        }

        public override int UpdateData(CustomSession item)
        {
            return CustomSessionDAL.GetInstance().Save(item, DataSaveOption.Update);
        }

        public override int DeleteData(int Id)
        {
            CustomSession csession = new CustomSession();
            csession.Id = Id;
            return CustomSessionDAL.GetInstance().Save(csession, DataSaveOption.Delete);
        }

        public int DeleteData(CustomSession item)
        {
            return CustomSessionDAL.GetInstance().Save(item, DataSaveOption.Delete);
        }

        public override List<CustomSession> LoadAllItemList()
        {
            return CustomSessionDAL.GetInstance().LoadItemList(new CustomSession(), DataLoadOption.LoadAll);
        }

        public override List<CustomSession> LoadItemList(CustomSession item)
        {
            return CustomSessionDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadAll);
        }

        public override List<CustomSession> LoadItemList(CustomSession item, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override CustomSession LoadItem(int id)
        {
            throw new NotImplementedException();
        }

        public CustomSession LoadItem(CustomSession item)
        {
            return CustomSessionDAL.GetInstance().LoadItem(item, DataLoadOption.LoadByCraiteria);
        }


        public static CustomSessionBAL GetInstance()
        {
            return new CustomSessionBAL();
        }
    }
}
