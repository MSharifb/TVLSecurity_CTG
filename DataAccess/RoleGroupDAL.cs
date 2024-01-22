using System;
using System.Collections.Generic;
using Entity;
using System.Data;
using DBClient;
using PrimaryBaseLibrary;

namespace DataAccess
{
   public class RoleGroupDAL : DataAccessBase<RoleGroup>
    {
        public override int Save(RoleGroup item, DataSaveOption sOption)
        {
            int id = 0;
            try
            {
                CustomParameterList paramList = this.GetSaveParamList(item, sOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                dbHelper.ExecuteNonQuery(paramList, "uspSetRoleGroups");
                id = dbHelper.IdentityValue;

            }
            catch (CaughtException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                CaughtException ce = new CaughtException(ex.Message, this, "Save");
                throw ce;
            }

            return id;
        }

        public override List<RoleGroup> LoadItemList(RoleGroup item, DataLoadOption lOption)
        {
            List<RoleGroup> gList = new List<RoleGroup>();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetRoleGroups");

                foreach (DataRow dr in dt.Rows)
                {
                    RoleGroup gInfo = new RoleGroup();
                    MapperBase.GetInstance().MapItem(gInfo, dr);
                    gList.Add(gInfo);
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

            return gList;
        }

        public override List<RoleGroup> LoadItemList(RoleGroup item, DataLoadOption lOption, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override RoleGroup LoadItem(RoleGroup item, DataLoadOption lOption)
        {
            RoleGroup gInfo = new RoleGroup();

            try
            {
                CustomParameterList paramList = this.GetLoadParamList(item, lOption);
                DBHelper dbHelper = DBHelper.GetInstance();
                DataTable dt = dbHelper.ExecuteQueryToDataTable(paramList, "uspGetRoleGroups");

                if(dt.Rows.Count> 0)
                {

                    MapperBase.GetInstance().MapItem(gInfo, dt.Rows[0]);
                   
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

            return gInfo;
        }


        private CustomParameterList GetLoadParamList(RoleGroup item, DataLoadOption lOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@LoadOption", lOption, DbType.Int32));
            paramList.Add(new CustomParameter("@RoleGroupsId", item.RoleGroupsId, DbType.Int32));
            return paramList;
        }

        private CustomParameterList GetSaveParamList(RoleGroup item, DataSaveOption sOption)
        {

            CustomParameterList paramList = new CustomParameterList();
            paramList.Add(new CustomParameter("@SaveOption", sOption, DbType.Int32));
            paramList.Add(new CustomParameter("@RoleGroupsId", item.RoleGroupsId, DbType.Int32));
            paramList.Add(new CustomParameter("@RoleGroupsTitle", item.RoleGroupsTitle, DbType.String));
            paramList.Add(new CustomParameter("@ApplicationId", item.ApplicationId, DbType.Int32));
            return paramList;
        }


        public static RoleGroupDAL GetInstance()
        {
            return new RoleGroupDAL();
        }

    }
}
