using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrimaryBaseLibrary;
using Entity;
using DBClient;
using System.Data;

namespace DataAccess
{
    public class ZoneDAL : DataAccessBase<Zone>
    {
        public override int Save(Zone item, DataSaveOption sOption)
        {
            throw new NotImplementedException();
        }

        public override List<Zone> LoadItemList(Zone item, DataLoadOption lOption)
        {
            List<Zone> zoneList = new List<Zone>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetZone");

                foreach (DataRow dr in dt.Rows)
                {
                    Zone zone = new Zone();
                    zone.ZoneId = Convert.ToInt32(dr["Id"].ToString());
                    zone.ZoneName = dr["ZoneName"].ToString();
                    zoneList.Add(zone);
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

            return zoneList;
        }

        public override List<Zone> LoadItemList(Zone item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override Zone LoadItem(Zone item, DataLoadOption lOption)
        {
            throw new NotImplementedException();
        }


        public int SaveEmployeeWithZone(string EmpId, List<Zone> zoneList, DataSaveOption sOption)
        {
            IDbTransaction transaction = null;
            int id = 0;

            try
            {

                IDbConnection con = DBHelper.GetConnection();
                con.Open();
                transaction = DBHelper.GetTransaction(con);

                try
                {
                    Zone userDelZone = new Zone();
                    userDelZone.EmpId = EmpId;

                    CustomParameterList paramListForDel = this.GetSaveParamList(userDelZone, DataSaveOption.Delete);
                    DBHelper dbHelper = DBHelper.GetInstance();
                    dbHelper.ExecuteNonQuery(paramListForDel, transaction, con, "uspSetUsersZone");

                   
                    foreach (var item in zoneList)
                    {
                        if (item.IsAssignedZone == true)
                        {
                            Zone obj = new Zone();
                            obj.EmpId = EmpId;
                            obj.ZoneId = item.ZoneId;

                            paramListForDel = this.GetSaveParamList(obj, DataSaveOption.Insert);
                            dbHelper.ExecuteNonQuery(paramListForDel, transaction, con, "uspSetUsersZone");
                        }
                    }


                    transaction.Commit();


                }
                catch (CaughtException ce)
                {
                    transaction.Rollback();
                    throw ce;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    CaughtException ce = new CaughtException(ex.Message, this, "Save");
                    throw ce;
                }
            }
            catch (CaughtException ce)
            {
                if (transaction != null)
                {
                    transaction.Rollback();

                }
                throw ce;

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                CaughtException ce = new CaughtException(ex.Message, this, "Save");
                throw ce;
            }

            return id;

        }

        public List<Zone> LoadUserZonebyEmpId(Zone item, DataLoadOption lOption)
        {

            List<Zone> userRoleList = new List<Zone>();

            try
            {
                CustomParameterList paramList = this.GetParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetUserZone");

                foreach (DataRow dr in dt.Rows)
                {
                    Zone userRoleInfo = new Zone();
                    MapperBase.GetInstance().MapItem(userRoleInfo, dr);
                    userRoleList.Add(userRoleInfo);
                }
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                CaughtException ce = new CaughtException(ex.Message, this, "LoadUserRole");
                throw ce;
            }

            return userRoleList;

        }

        private CustomParameterList GetSaveParamList(Zone item, DataSaveOption sOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@SaveOption", sOption, DbType.Int32));
            paramList.Add(new CustomParameter("@EmpId", item.EmpId, DbType.String));
            paramList.Add(new CustomParameter("@ZoneId", item.ZoneId, DbType.Int32));
            paramList.Add(new CustomParameter("@CreatedBy", item.CreatedBy, DbType.String));
            paramList.Add(new CustomParameter("@CreatedDate", item.CreatedDate, DbType.DateTime));
            paramList.Add(new CustomParameter("@UpdatedBy", item.UpdatedBy, DbType.String));
            return paramList;

        }

        private CustomParameterList GetLoadParamList(Zone item, DataLoadOption lOption)
        {
            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            paramList.Add(new CustomParameter("@ZoneId", item.ZoneId, DbType.Int32));
            return paramList;
        }

        private CustomParameterList GetParamList(Zone item, DataLoadOption lOption)
        {
            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            paramList.Add(new CustomParameter("@EmpId", item.EmpId, DbType.Int32));
            return paramList;
        }

        public static ZoneDAL GetInstance()
        {
            return new ZoneDAL();
        } 
    }
}
