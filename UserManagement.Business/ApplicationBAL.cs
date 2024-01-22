using System;
using Entity;
using DataAccess;
using System.Collections.Generic;
using PrimaryBaseLibrary;

namespace UserManagement.Business
{
    public class ApplicationBAL : BusinessBase<Application>
    {

        public override int InsertData(Application item)
        {
            try
            {
                return ApplicationDAL.GetInstance().Save(item, DataSaveOption.Insert);
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
        }

        public override int UpdateData(Application item)
        {
            return ApplicationDAL.GetInstance().Save(item, DataSaveOption.Update);
        }

        public override int DeleteData(int Id)
        {
            Application app = new Application();
            app.ApplicationId = Id;
            return ApplicationDAL.GetInstance().Save(app, DataSaveOption.Delete);
        }

        public override List<Application> LoadAllItemList()
        {
            return ApplicationDAL.GetInstance().LoadItemList(new Application(), DataLoadOption.LoadAll);
        }

        public override List<Application> LoadItemList(Application item)
        {
            return ApplicationDAL.GetInstance().LoadItemList(new Application(), DataLoadOption.LoadByCraiteria);
        }

        public override List<Application> LoadItemList(Application item, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Application LoadItem(int id)
        {
            Application app = new Application();
            app.ApplicationId = id;
            return ApplicationDAL.GetInstance().LoadItem(app, DataLoadOption.LoadByCraiteria);
        }

        public static ApplicationBAL GetInstance()
        {
            return new ApplicationBAL();
        }
    }
}
