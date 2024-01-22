using System;
using System.Collections.Generic;
using Entity;
using DataAccess;
using PrimaryBaseLibrary;

namespace UserManagement.Business
{
    public class UserGroupBAL : BusinessBase<UserGroup>
    {
        public override int InsertData(UserGroup item)
        {
           return  UserGroupDAL.GetInstance().Save(item,DataSaveOption.Insert);
        }

        public int InsertData(UserGroup item, List<Role> roleList)
        {
            return UserGroupDAL.GetInstance().Save(item, roleList, DataSaveOption.Insert);
        }

        public override int UpdateData(UserGroup item)
        {
           return  UserGroupDAL.GetInstance().Save(item, DataSaveOption.Update);
        }

        public int UpdateData(UserGroup item,List<Role> roleList)
        {
            return UserGroupDAL.GetInstance().Save(item,roleList,DataSaveOption.Update);
        }


        public override int DeleteData(int Id)
        {
            UserGroup group = new UserGroup();
            group.GroupId = Id;
           return  UserGroupDAL.GetInstance().Save(group, DataSaveOption.Delete);
        }


        public override List<UserGroup> LoadItemList(UserGroup item)
        {
            throw new NotImplementedException();

        }

        public override List<UserGroup> LoadAllItemList()
        {
            UserGroup item = new UserGroup();
            return UserGroupDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadAll);

        }

        public override List<UserGroup> LoadItemList(UserGroup item, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override UserGroup LoadItem(int id)
        {
            UserGroup group = new UserGroup();
            group.GroupId = id;

            return UserGroupDAL.GetInstance().LoadItem(group, DataLoadOption.LoadByCraiteria);
        }

        public static UserGroupBAL GetInstance()
        {
            return new UserGroupBAL();
        }
    }
}
