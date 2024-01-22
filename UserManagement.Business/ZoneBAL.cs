using System;
using DataAccess;
using Entity;
using System.Collections.Generic;
using PrimaryBaseLibrary;

namespace UserManagement.Business
{
    public class ZoneBAL : BusinessBase<Zone>
    {
        public override int InsertData(Zone item)
        {
            throw new NotImplementedException();
        }
        public override int UpdateData(Zone item)
        {
            throw new NotImplementedException();
        }

        public override int DeleteData(int Id)
        {
            throw new NotImplementedException();
        }

        public override List<Zone> LoadAllItemList()
        {
            return ZoneDAL.GetInstance().LoadItemList(new Zone(), DataLoadOption.LoadAll);
        }
        public override List<Zone> LoadItemList(Zone item)
        {
            return ZoneDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadByCraiteria);
        }

        public override List<Zone> LoadItemList(Zone item, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Zone LoadItem(int id)
        {
            throw new NotImplementedException();
        }

        public List<Zone> LoadZoneListByEmpID(string EmpID)
        {
            Zone zone = new Zone();
            zone.EmpId = EmpID;
            return ZoneDAL.GetInstance().LoadUserZonebyEmpId(zone, DataLoadOption.LoadAll);
        }

        public int InsertEmpZone(string EmpId, List<Zone> roleList)
        {
            try
            {
                return ZoneDAL.GetInstance().SaveEmployeeWithZone(EmpId, roleList, DataSaveOption.Insert);
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
        }


        public static ZoneBAL GetInstance()
        {
            return new ZoneBAL();
        }
    }
}
