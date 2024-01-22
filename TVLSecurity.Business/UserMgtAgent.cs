using PrimaryBaseLibrary;
using Entity;
using System;
using System.Collections.Generic;



namespace TVLSecurity.Business
{
    public class UserMgtAgent
    {

        #region User Info Operation
        public static Int32 InsertUserData(User user)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.InsertUserData(user);
            }


        }

        public static void DeleteUserData(int id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                umService.DeleteUserData(id);
            }
        }

        public static int UpdateUserData(User user)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.UpdateUserData(user);
            }
        }

        public static int InserUserRoles(int userId, List<Role> roleList, int applicationId, int moduleId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.InsertUserRoles(userId, roleList, applicationId, moduleId);
            }
        }

        public static List<User> GetUserList()
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                List<User> list = umService.GetUserList();
                return list;
            }

        }

        public static Entity.User GetUser(int id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetUser(id);
            }
        }

        public static Entity.User GetUserByLoginId(string loginId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetUserByLoginId(loginId);
            }
        }

        public static List<Entity.User> GetUserListByCraiteria(User user)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetUserListByCraiteria(user);
            }
        }

        #endregion

        #region UserRole Info Operation

        //public static Entity.UserRoleInfo GetUserRole(int id)
        //{
        //    using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
        //    {
        //        return umService.GetUserRole(id);
        //    }
        //}

        public static List<UserRoleInfo> GetUserRole(int id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetUserRole(id);
            }
        }

        #endregion

        #region Group Operation

        public static Int32 CreateGroup(UserGroup item, List<Role> roleList)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.CreateGroup(item, roleList);
            }
        }

        public static Int32 UpdateGroup(UserGroup item, List<Role> roleList)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.UpdateGroup(item, roleList);
            }
        }


        public static Int32 DeleteGroup(int id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.DeleteGroup(id);
            }
        }

        public static UserGroup GetUserGroupById(int id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetGroupById(id);
            }
        }

        public static List<UserGroup> GetGroupList()
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetGroupList();
            }
        }

        #endregion

        #region Roles
        public static List<Role> GetRoleList()
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRolesList();
            }
        }

        public static List<Role> GetRoleListByCraiteria(Role role)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRolesListByCraiteria(role);
            }
        }

        public static List<RoleGroup> GetRoleGroupList()
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRoleGroups();
            }
        }

        public static List<Role> GetRoleListByUser(int userId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRolesListByUser(userId);
            }
        }

        public static List<Role> GetRolesListByUserGroup(int userGroupId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRolesListByUserGroup(userGroupId);
            }
        }

        public static Role GetRoleById(int roleId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRoleById(roleId);
            }
        }

        public static int InsertRole(Role role, List<Menu> menuList, List<Right> rightList)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.InsertRolesMenusRight(role, menuList, rightList);
            }
        }

        public static int UpdateRole(Role role, List<Menu> menuList, List<Right> rightList)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.UpdateRolesMenusRight(role, menuList, rightList);
            }
        }

        public static Int32 CreateRoleGroup(RoleGroup item)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.CreateRoleGroup(item);
            }
        }


        public static Int32 UpdateRoleGroup(RoleGroup item)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.UpdateRoleGroup(item);
            }
        }


        public static Int32 DeleteRoleGroup(int Id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.DeleteRoleGroup(Id);
            }
        }


        public static RoleGroup GetRoleGroupById(int Id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRoleGroupById(Id);
            }
        }



        #endregion

        #region Menus


        public static int InsertMenuData(Menu menu)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.InsertMenuData(menu);
            }
        }


        public static int UpdateMenuData(Menu menu)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.UpdateMenuData(menu);
            }
        }


        public static int DeleteMenuData(int id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.DeleteMenuData(id);
            }
        }

        public static List<Menu> GetAllMenus()
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetAllMenus();
            }
        }

        public static List<Menu> GetMenusByParent(int parentId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenusByParent(parentId);
            }
        }

        public static List<Menu> GetMenusByRoleId(int roleId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenuListByRoleId(roleId);
            }
        }


        public static Menu GetMenusByMenuId(int menuId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenuByMenuId(menuId);
            }
        }

        public static Menu GetMenuByMenuNameAndLoginId(string loginId, string MenuName)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenuByMenuNameAndLoginId(loginId, MenuName);
            }

        }

        public static List<Menu> GetMenusByApplicationAndModuleId(int roleId, int applicationId, int moduleId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenusByApplicationAndModuleId(roleId, applicationId, moduleId);
            }
        }


        public static List<Menu> GetMenusByApplicationAndModuleName(string appName, string modName)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenusByApplicationAndModuleName(appName, modName);
            }
        }

        public static List<Menu> GetMenus(string loginId, string appName, string modName)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenus(loginId, appName, modName);
            }
        }
        public static List<Menu> GetParentMenus(int ModelId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetParentMenus(ModelId);
            }
        }





        #endregion

        #region Rights
        public static List<Right> GetAllRights()
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetAllRights();
            }
        }

        public static List<Right> GetAllRightsMapedByRole(int roleId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetAllRightsMapedByRole(roleId);
            }
        }

        public static Right GetRightByLoginIdAndRightName(string loginId, string rightName)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRightByLoginIdAndRightName(loginId, rightName);
            }

        }

        public static List<Right> GetRightByRoleAndAppAndModule(int roleId, int appId, int moduleId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRightsByRoleAndAppAndModule(roleId, appId, moduleId);
            }

        }


        #endregion

        #region "Department"
        public static List<Department> GetAllDepartment()
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetAllDepartment();
            }
        }
        #endregion

        #region "Zone"
        public static List<Zone> GetZoneList()
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetZoneList();
            }
        }

        public static int InsertEmpZone(string empId, List<Zone> zoneList)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.InsertEmpZone(empId, zoneList);
            }
        }

        public static List<Zone> LoadZoneListByEmpID(string empId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.LoadZoneListByEmpID(empId);
            }
        }

        #endregion

        #region "Employee"
        public static List<Employee> GetAllEmployee()
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetAllEmployee();
            }
        }

        public static List<Employee> GetAllEmployeeWithPaging(int startRowIndex, int maxRow)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetAllEmployeeWithPaging(startRowIndex, maxRow);
            }
        }


        public static List<Employee> GetSearchEmployeeWithPaging(Employee obj, int startRowIndex, int maxRow, out int intTotalRowCount)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetSearchEmployeeWithPaging(out intTotalRowCount, obj, startRowIndex, maxRow);
            }
        }


        public static Int32 GetTotalEmployeeCount()
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetTotalEmployeeCount();
            }
        }

        #endregion

        #region Application
        public static List<Application> GetAllApplications()
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetAllApplications();
            }
        }


        public static Application GetApplicationById(int id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetApplicationById(id);
            }
        }


        public static int CreateApplication(Application app)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.InsertApplicationData(app);
            }
        }

        public static int UpdateApplication(Application app)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.UpdateApplicationData(app);
            }
        }
        #endregion

        #region Module
        public static List<Module> GetAllModules()
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetAllModules();
            }
        }

        public static List<Module> GetModulesByApplicationId(int appId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetModulesByApplicationId(appId);
            }
        }

        public static List<Menu> GetMenusByModuleId(int ModuleId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetParentMenus(ModuleId);
            }
        }

        public static Module GetModuleById(int id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetModuleById(id);

            }
        }


        public static int CreateModule(Module module)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.InsertModuleData(module);
            }
        }

        public static int UpdateModule(Module module)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.UpdateModuleData(module);
            }
        }
        #endregion

        #region Dashboard
        public static List<Dashboard> GetZoneWiseUserList()
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetZoneWiseUserList();
            }
        }        
        #endregion

        #region Validation

        public static IEnumerable<RuleViolation> GetUserRuleViolations(User user)
        {
            return null;
        }

        public void ValidateUser(User user)
        {

        }

        #endregion

        #region  UserMenu
        public static int InsertUserMenu(UserMenu userMenu, List<Menu> menuList)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.InsertUserMenus(userMenu, menuList);
            }
        }

        public static int UpdateUserMenu(UserMenu userMenu, List<Menu> menuList)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.UpdateUserMenus(userMenu, menuList);
            }
        }

        public static List<Menu> LoadUserMenuList(UserMenu userMenu)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.LoadUserMenuList(userMenu);
            }
        }

        public static List<UserMenu> GetAllUserMenuList(UserMenu userMenu)
        {
            using (UserMgtService.UserManagementServiceClient umService = new TVLSecurity.Business.UserMgtService.UserManagementServiceClient())
            {
                return umService.GetAllUserMenuList(userMenu);
            }
        }
        #endregion 
    }
}
