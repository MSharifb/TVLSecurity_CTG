using System;
using Entity;
using DataAccess;
using System.Collections.Generic;
using PrimaryBaseLibrary;

namespace UserManagement.Business
{
    public class ModuleBAL : BusinessBase<Module>
    {
        public override int InsertData(Module item)
        {
           return ModuleDAL.GetInstance().Save(item, DataSaveOption.Insert);
        }

        public override int UpdateData(Module item)
        {
            return ModuleDAL.GetInstance().Save(item, DataSaveOption.Update);
        }

        public override int DeleteData(int Id)
        {
            Module module = new Module();
            module.ModuleId = Id;
            return ModuleDAL.GetInstance().Save(module, DataSaveOption.Delete);
        }

        public override List<Module> LoadAllItemList()
        {
            return ModuleDAL.GetInstance().LoadItemList(new Module(), DataLoadOption.LoadAll);
        }

        public override List<Module> LoadItemList(Module item)
        {
            return ModuleDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadByCraiteria);
        }

        public override List<Module> LoadItemList(Module item, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Module LoadItem(int id)
        {
            Module module = new Module();
            module.ModuleId = id;
            return ModuleDAL.GetInstance().LoadItem(module, DataLoadOption.LoadByCraiteria);
        }

        public static ModuleBAL GetInstance()
        {
            return new ModuleBAL();
        }
    }
}
