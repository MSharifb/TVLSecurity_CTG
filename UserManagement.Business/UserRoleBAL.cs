using System;
using Entity;
using DataAccess;
using System.Collections.Generic;
using PrimaryBaseLibrary;

namespace UserManagement.Business
{
   public class UserRoleBAL : BusinessBase<UserRole>
    {
        public override int InsertData(UserRole item)
        {
            try
            {
                return UserRoleDAL.GetInstance().Save(item, DataSaveOption.Insert);
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
        }

        public  int InsertUserRoles(int userId,List<Role> roleList,int applicationId,int moduleId)
        {
            try
            {
                return UserRoleDAL.GetInstance().SaveUserRolesWithTran(userId,roleList,applicationId,moduleId, DataSaveOption.Insert);
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
        }

        public override int UpdateData(UserRole item)
        {
            throw new NotImplementedException();
        }

        public override int DeleteData(int Id)
        {
            throw new NotImplementedException();
        }

        public  int DeleteUserData(int Id)
        {
            UserRole userRole = new UserRole();
            userRole.UserId = Id;
            try
            {
                return UserRoleDAL.GetInstance().Save(userRole, DataSaveOption.Delete);
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
        }

        //public UserRoleInfo LoadUserRole(int Id)
        //{
        //    UserRoleInfo userRole = new UserRoleInfo();
        //    userRole.UserId = Id;
        //    return UserRoleDAL.GetInstance().LoadUserRole(userRole, DataLoadOption.LoadByCraiteria);
        //}

        public List<UserRoleInfo> LoadUserRole(int Id)
        { 
            UserRoleInfo userRole = new UserRoleInfo();
            userRole.UserId = Id;
            return UserRoleDAL.GetInstance().LoadUserRole(userRole, DataLoadOption.LoadByCraiteria);
        }

        public override List<UserRole> LoadAllItemList()
        {
            throw new NotImplementedException();
        }

        public override List<UserRole> LoadItemList(UserRole item)
        {
            throw new NotImplementedException();
        }

        public override List<UserRole> LoadItemList(UserRole item, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override UserRole LoadItem(int id)
        {
            throw new NotImplementedException();
        }

        public static UserRoleBAL GetInstance()
        {
            return new UserRoleBAL();
        }
    }
}
