using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using UserManagement.Business;
using System.Security.Cryptography;


namespace UserManagementService
{
    // NOTE: If you change the class name "UserManagementService" here, you must also update the reference to "UserManagementService" in Web.config and in the associated .svc file.
    public class UserManagementService : IUserManagementService
    {

        #region User Info Operation
        public Int32 InsertUserData(User user)
        {
            return UserBAL.GetInstance().InsertData(user);
        }

        public void DeleteUserData(int id)
        {
            UserBAL.GetInstance().DeleteData(id);
        }

        public int UpdateUserData(User user)
        {
            return UserBAL.GetInstance().UpdateData(user);
        }


        public List<User> GetUserList()
        {
            List<User> list = UserBAL.GetInstance().LoadAllItemList();
            return list;

        }


        public Entity.User GetUser(int id)
        {
            return UserBAL.GetInstance().LoadItem(id);
        }

        public Int32 InsertUserRoles(int userId, List<Role> roleList, int applicationId, int moduleId)
        {
            return UserRoleBAL.GetInstance().InsertUserRoles(userId, roleList, applicationId, moduleId);
        }

        public User GetUserByLoginId(string loginId)
        {
            return UserBAL.GetInstance().LoadByLoginId(loginId);
        }

        public List<User> GetUserListByCraiteria(User user)
        {
            return UserBAL.GetInstance().LoadItemList(user);
        }


        #endregion

        #region UserRole Info Operation

        //public Entity.UserRoleInfo GetUserRole(int id)
        //{
        //    return UserRoleBAL.GetInstance().LoadUserRole(id);
        //}

        public List<UserRoleInfo> GetUserRole(int id)
        {
            return UserRoleBAL.GetInstance().LoadUserRole(id);
        }

        #endregion

        #region Group Operation


        public Int32 CreateGroup(UserGroup item, List<Role> roleList)
        {
            return UserGroupBAL.GetInstance().InsertData(item, roleList);
        }


        public Int32 UpdateGroup(UserGroup item, List<Role> roleList)
        {
            return UserGroupBAL.GetInstance().UpdateData(item, roleList);
        }


        public Int32 DeleteGroup(int id)
        {
            return UserGroupBAL.GetInstance().DeleteData(id);
        }

        public List<UserGroup> GetGroupList()
        {
            return UserGroupBAL.GetInstance().LoadAllItemList();
        }

        public UserGroup GetGroupById(int id)
        {
            return UserGroupBAL.GetInstance().LoadItem(id);
        }

        #endregion

        #region Role Operation

        public List<Role> GetRolesList()
        {
            return RoleBAL.GetInstance().LoadAllItemList();
        }

        public List<Role> GetRolesListByCraiteria(Role role)
        {
            return RoleBAL.GetInstance().LoadItemList(role);
        }

        public List<Role> GetRolesListByUser(int userId)
        {
            return RoleBAL.GetInstance().LoadtemListByUser(userId);
        }

        public List<Role> GetRolesListByUserGroup(int groupId)
        {
            Role role = new Role();
            role.UserGroupId = groupId;
            return RoleBAL.GetInstance().LoadItemList(role);
        }

        public Int32 InsertRolesMenusRight(Role role, List<Menu> menuList, List<Right> rightList)
        {
            return RoleBAL.GetInstance().InsertRoleMnuRight(role, menuList, rightList);
        }

        public Int32 UpdateRolesMenusRight(Role role, List<Menu> menuList, List<Right> rightList)
        {
            return RoleBAL.GetInstance().UpdateRoleMnuRight(role, menuList, rightList);
        }

        public List<RoleGroup> GetRoleGroups()
        {
            return RoleGroupBAL.GetInstance().LoadAllItemList();
        }

        public Role GetRoleById(int id)
        {
            return RoleBAL.GetInstance().LoadItem(id);
        }


        public Int32 CreateRoleGroup(RoleGroup item)
        {
            return RoleGroupBAL.GetInstance().InsertData(item);
        }


        public Int32 UpdateRoleGroup(RoleGroup item)
        {
            return RoleGroupBAL.GetInstance().UpdateData(item);
        }


        public Int32 DeleteRoleGroup(int Id)
        {
            return RoleGroupBAL.GetInstance().DeleteData(Id);
        }


        public RoleGroup GetRoleGroupById(int id)
        {
            return RoleGroupBAL.GetInstance().LoadItem(id);
        }

        #endregion

        #region Menu Operation



        public int InsertMenuData(Menu menu)
        {
            return MenuBAL.GetInstance().InsertData(menu);
        }


        public int UpdateMenuData(Menu menu)
        {
            return MenuBAL.GetInstance().UpdateData(menu);
        }


        public int DeleteMenuData(int id)
        {
            return MenuBAL.GetInstance().DeleteData(id);
        }

        public List<Menu> GetAllMenus()
        {
            return MenuBAL.GetInstance().LoadAllItemList();
        }


        public List<Menu> GetMenusByParent(int parentId)
        {
            return MenuBAL.GetInstance().LoadMenuListByParent(parentId);
        }

        public List<Menu> GetMenuListByRoleId(int roleId)
        {
            return MenuBAL.GetInstance().LoadAllItemListByRoleId(roleId);
        }

        public List<Menu> GetParentMenuListByLoginId(string loginId)
        {
            return MenuBAL.GetInstance().LoadMenuListByLoginId(loginId);
        }

        public List<Menu> GetChildMenuListByLoginAndParentId(string loginId, int parentId)
        {
            Menu menu = new Menu();
            menu.LoginId = loginId;
            menu.ParentMenuId = parentId;
            return MenuBAL.GetInstance().LoadItemList(menu);

        }

        public Menu GetMenuByMenuNameAndLoginId(string loginId, string MenuName)
        {
            Menu menu = new Menu();
            menu.LoginId = loginId;
            menu.MenuName = MenuName.Trim();
            return MenuBAL.GetInstance().LoadItem(menu);
        }

        public Menu GetMenuByMenuName(string MenuName)
        {
            Menu menu = new Menu();
            menu.MenuName = MenuName.Trim();
            return MenuBAL.GetInstance().LoadItem(menu);
        }

        public Menu GetMenuByMenuId(int id)
        {
            Menu menu = new Menu();
            menu.MenuId = id;
            return MenuBAL.GetInstance().LoadItem(menu);

        }

        public List<Menu> GetMenusByApplicationAndModuleId(int roleId, int applicationId, int moduleId)
        {
            Menu menu = new Menu();
            menu.ApplicationId = applicationId;
            menu.ModuleId = moduleId;
            menu.RoleId = roleId;

            return MenuBAL.GetInstance().LoadItemList(menu);
        }

        public List<Menu> GetMenus(string loginId, string appName, string moduleName)
        {
            Menu menu = new Menu();
            menu.ApplicationName = appName;
            menu.ModuleName = moduleName;
            menu.LoginId = loginId;

            return MenuBAL.GetInstance().LoadItemList(menu);
        }

        public List<Menu> GetParentMenus(int ModuleId)
        {
            Menu menu = new Menu();
            menu.ModuleId = ModuleId;

            return MenuBAL.GetInstance().LoadItemList(menu);
        }

        public List<Menu> GetMenusByApplicationAndModuleName(string appName, string modName)
        {
            Menu menu = new Menu();
            menu.ApplicationName = appName;
            menu.ModuleName = modName;

            return MenuBAL.GetInstance().LoadItemList(menu);
        }

        #endregion

        #region Right Operation

        public List<Right> GetAllRights()
        {
            return RightBAL.GetInstance().LoadAllItemList();
        }

        public List<Right> GetAllRightsMapedByRole(int roleId)
        {
            return RightBAL.GetInstance().LoadRightsMapedByRole(roleId);
        }

        public Right GetRightByLoginIdAndRightName(string loginId, string rightName)
        {
            return RightBAL.GetInstance().LoadRightByLoginIdAndRightName(loginId, rightName);
        }

        public List<Right> GetRightsByRoleAndAppAndModule(int roleId, int appId, int moduleId)
        {
            Right right = new Right();
            right.RoleId = roleId;
            right.ApplicationId = appId;
            right.ModuleId = moduleId;

            return RightBAL.GetInstance().LoadItemList(right);
        }


        #endregion

        #region Department
        public List<Department> GetAllDepartment()
        {
            return DepartmentBAL.GetInstance().LoadAllItemList();
        }
        #endregion

        #region Zone

        public List<Zone> GetZoneList()
        {
            return ZoneBAL.GetInstance().LoadAllItemList();
        }
        public Int32 InsertEmpZone(string empID, List<Zone> roleList)
        {
            return ZoneBAL.GetInstance().InsertEmpZone(empID, roleList);
        }
        public List<Zone> LoadZoneListByEmpID(string empId)
        {
            return ZoneBAL.GetInstance().LoadZoneListByEmpID(empId);
        }

        #endregion

        #region Employee
        public List<Employee> GetEmployeesByApplicationId(int applicationId)
        {
            Employee emp = new Employee();
            emp.ApplicationId = applicationId;
            return EmployeeBAL.GetInstance().LoadItemList(emp);
        }

        public List<Employee> GetAllEmployee()
        {
            return EmployeeBAL.GetInstance().LoadAllItemList();
        }

        public Int32 GetTotalEmployeeCount()
        {
            return EmployeeBAL.GetInstance().GetTotalEmploeeCount(new Employee());
        }

        public List<Employee> GetAllEmployeeWithPaging(int startRowIndex, int maxRows)
        {
            return EmployeeBAL.GetInstance().LoadItemList(new Employee(), startRowIndex, maxRows);
        }

        public List<Employee> GetSearchEmployeeWithPaging(Employee obj, int startRowIndex, int maxRows, out int intTotalRowCount)
        {
            return EmployeeBAL.GetInstance().LoadSearchItemList(obj, startRowIndex, maxRows, out intTotalRowCount);
        }

        #endregion

        #region Validation
        //public void ValidateUser(User user)
        //{

        //}

        public bool ValidateUser(string userId, string password)
        {
            var user = GetUserByLoginId(userId);
            if (verifyMd5Hash(password, getMd5Hash(password)))
                return true;
            else return false;

        }
        #endregion

        #region "Application"
        public List<Application> GetAllApplications()
        {
            return ApplicationBAL.GetInstance().LoadAllItemList();
        }


        public Application GetApplicationById(int id)
        {
            return ApplicationBAL.GetInstance().LoadItem(id);
        }

        public int InsertApplicationData(Application app)
        {

            return ApplicationBAL.GetInstance().InsertData(app);

        }

        public int UpdateApplicationData(Application app)
        {
            return ApplicationBAL.GetInstance().UpdateData(app);
        }

        public int DeleteApplicationData(int id)
        {
            return ApplicationBAL.GetInstance().DeleteData(id);
        }
        #endregion

        #region "Module"
        public List<Module> GetAllModules()
        {
            return ModuleBAL.GetInstance().LoadAllItemList();
        }

        public List<Module> GetModulesByApplicationId(int appId)
        {
            Module module = new Module();
            module.ApplicationId = appId;
            return ModuleBAL.GetInstance().LoadItemList(module);
        }


        public Module GetModuleById(int id)
        {
            return ModuleBAL.GetInstance().LoadItem(id);
        }

        public int InsertModuleData(Module module)
        {
            return ModuleBAL.GetInstance().InsertData(module);
        }

        public int UpdateModuleData(Module module)
        {
            return ModuleBAL.GetInstance().UpdateData(module);
        }

        public int DeleteModuleData(int id)
        {
            return ModuleBAL.GetInstance().DeleteData(id);
        }

        #endregion

        #region Dashboard       
        public List<Dashboard> GetZoneWiseUserList()
        {
            return DashboardBAL.GetInstance().LoadAllItemList();
        }

        #endregion

        #region Service Utils

        // a 32 character hexadecimal string.
        public string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
                //sBuilder.Append(Convert.ToString(data[i], 2).PadLeft(8, '0')); //Convert into binary
            }

            // Return the hexadecimal string.
            return sBuilder.ToString().ToLower();
        }

        // Verify a hash against a string.
        public bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = getMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        #endregion

        #region UserMenu
        public Int32 InsertUserMenus(UserMenu userMenu, List<Menu> menuList)
        {
            return UserMenuBAL.GetInstance().InsertUserMenus(userMenu, menuList);
        }

        public Int32 UpdateUserMenus(UserMenu userMenu, List<Menu> menuList)
        {
            return UserMenuBAL.GetInstance().UpdateUserMenus(userMenu, menuList);
        }
        public List<Menu> LoadUserMenuList(UserMenu userMenu)
        {
            return UserMenuBAL.GetInstance().LoadUserMenuList(userMenu);
        }
        public List<UserMenu> GetAllUserMenuList(UserMenu userMenu)
        {
            return UserMenuBAL.GetInstance().LoadItemList(userMenu);
        }
        #endregion
    }
}
