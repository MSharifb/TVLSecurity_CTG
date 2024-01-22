using System;
using Entity;
using DataAccess;
using System.Collections.Generic;
using PrimaryBaseLibrary;


namespace UserManagement.Business
{
    public class UserBAL : BusinessBase<User>
    {
             
        public static UserBAL GetInstance()
        {
            return new UserBAL();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(User item)
        {
            try
            {
                return UserDAL.GetInstance().Save(item, DataSaveOption.Insert);
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
        }

        /// <summary>
        /// Update Data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        public override int UpdateData(User item)
        {
            try
            {
                return UserDAL.GetInstance().Save(item, DataSaveOption.Update);
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// Delete Data
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public override int DeleteData(int Id)
        {
            Entity.User item = new User();
            item.UserId = Id;
            return UserDAL.GetInstance().Save(item, DataSaveOption.Delete);
        }

        #endregion

        #region Load Data


        public override List<User> LoadItemList(User item)
        {
            return UserDAL.GetInstance().LoadItemList(item, DataLoadOption.LoadByCraiteria);
        }

        public override List<User> LoadItemList(User item, int startRowIndex, int maxRow)
        {
            throw new NotImplementedException();
        }

        public override User LoadItem(int Id)
        {

            User user = new User();
            user.UserId = Id;
            return UserDAL.GetInstance().LoadItem(user, DataLoadOption.LoadByCraiteria);
        }

        public User LoadByLoginId(string loginId)
        {

            User user = new User();
            user.LoginId = loginId;
            return UserDAL.GetInstance().LoadItem(user, DataLoadOption.LoadByCraiteria);
        }

        public override System.Collections.Generic.List<User> LoadAllItemList()
        {

            UserDAL UserDal = new UserDAL();
            return UserDal.LoadItemList(new User(), DataLoadOption.LoadAll);
        }

        #endregion

        #region Validation
        public IEnumerable<RuleViolation> GetRuleViolations(User user)
        {

            if (String.IsNullOrEmpty(user.LoginId))
                yield return new RuleViolation("Login Id required", "LoginId");

            if (String.IsNullOrEmpty(user.FirstName))
                yield return new RuleViolation("First Name required", "FirstName");

            if (String.IsNullOrEmpty(user.LastName))
                yield return new RuleViolation("Last Name required", "Last Name");

            if (String.IsNullOrEmpty(user.Password))
                yield return new RuleViolation("Password Required", "Password");
            else
            {
                if (user.Password.Length < 6)
                    yield return new RuleViolation("Password length should be greater than 6", "Password");
            }

            yield break;
        }
        #endregion
    }
}
