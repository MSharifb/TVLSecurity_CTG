using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using TVLSecurity.Business;
using System.Collections;
using DataAccess;

namespace TVLSecurity.Web.Models
{
    public class UserRepository
    {
        
        #region User Info
        public static Int32 InsertUserData(User user)
        {
            return UserMgtAgent.InsertUserData(user);
        }

        public static void DeleteUserData(int id)
        {
            UserMgtAgent.DeleteUserData(id);
        }

        public static int UpdateUserData(User user)
        {
            return UserMgtAgent.UpdateUserData(user);
        }

        public static void InsertUserRolesData(int userId, List<Role> roleList,int applicationId,int moduleId)
        {
            UserMgtAgent.InserUserRoles(userId, roleList, applicationId,moduleId);
        }

        public static List<User> GetUserList()
        {
            List<User> list = UserMgtAgent.GetUserList();
            return list;

        }

        public static List<User> GetUserListByCraiteria(User user)
        {
            if (!string.IsNullOrEmpty(user.LoginId))
            {
                user.LoginId = user.LoginId.Trim();
            }


            if (!string.IsNullOrEmpty(user.LastName))
            {
                user.LastName = user.LastName.Trim();
            }
           
            
            List<User> list = UserMgtAgent.GetUserListByCraiteria(user);
            return list;

        }


        public static Entity.User GetUser(int id)
        {
            return UserMgtAgent.GetUser(id);
        }

        public static Entity.User GetUserByLoginId(string loginId)
        {
            return UserMgtAgent.GetUserByLoginId(loginId);
        }


        #endregion

        #region Group

        public static Int32 CreateGroup(UserGroup item,List<Role> roleList)
        {
            return UserMgtAgent.CreateGroup(item,roleList);
        }

        public static Int32 UpdateGroup(UserGroup item,List<Role> roleList)
        {
            return UserMgtAgent.UpdateGroup(item, roleList);
        }


        public static Int32 DeleteGroup(int id)
        {
            return UserMgtAgent.DeleteGroup(id);
        }

        public static List<UserGroup> GetGroupList()
        {
            return UserMgtAgent.GetGroupList();
        }

        public static UserGroup GetUserGroupById(int id)
        {
            return UserMgtAgent.GetUserGroupById(id);
        }

        #endregion

        #region Role

        public static List<Menu> GetMenusByRoleId(int roleId)
        {
            return UserMgtAgent.GetMenusByRoleId(roleId);
            
        }


        public static List<Role> GetRoleListByCraiteria(Role role)
        {
            return UserMgtAgent.GetRoleListByCraiteria(role);
        }


        public static int InsertRole(Role role, List<Menu> menuList, List<Right> rightList)
        {
            return UserMgtAgent.InsertRole(role, menuList, rightList);
        }

        public static int UpdateRole(Role role, List<Menu> menuList, List<Right> rightList)
        {
            return UserMgtAgent.UpdateRole(role, menuList, rightList);
        }

        public static List<Role> GetRoles()
        {
            return UserMgtAgent.GetRoleList();
        }

        public static List<RoleGroup> GetRoleGroups()
        {
            return UserMgtAgent.GetRoleGroupList();
        }


        public static List<Role> GetRolesByUser(int userId)
        {
            return UserMgtAgent.GetRoleListByUser(userId);
        }
        public static List<Role> GetRolesByUserGroup(int userGroupId)
        {
            return UserMgtAgent.GetRolesListByUserGroup(userGroupId);
        }

        public static Role GetRoleById(int roleId)
        {
            return UserMgtAgent.GetRoleById(roleId);
        }


        public static Int32 CreateRoleGroup(RoleGroup item)
        {
            return UserMgtAgent.CreateRoleGroup(item);
        }


        public static Int32 UpdateRoleGroup(RoleGroup item)
        {
            return UserMgtAgent.UpdateRoleGroup(item);
        }


        public static Int32 DeleteRoleGroup(int Id)
        {
            return UserMgtAgent.DeleteRoleGroup(Id);
        }


        public static RoleGroup GetRoleGroupById(int Id)
        {
            return UserMgtAgent.GetRoleGroupById(Id);
        }

        #endregion

        #region Menus


        public static int InsertMenuData(Menu menu)
        {
            return UserMgtAgent.InsertMenuData(menu);
        }


        public static int UpdateMenuData(Menu menu)
        {
            return UserMgtAgent.UpdateMenuData(menu);
        }


        public static int DeleteMenuData(int id)
        {
            return UserMgtAgent.DeleteMenuData(id);
        }

        public static List<Menu> GetAllMenus()
        {
            return UserMgtAgent.GetAllMenus();
        }

        public static List<Menu> GetMenusByParent(int parentId)
        {
            return UserMgtAgent.GetMenusByParent(parentId);
        }

        public static List<Menu> GetMenuListByLoginId(string loginId)
        {
            return UserMgtAgent.GetMenus(loginId,"","");
        }

        public static List<Menu> GetMenusByApplicationAndModuleId(int roleId,int applicationId, int moduleId)
        {
            return UserMgtAgent.GetMenusByApplicationAndModuleId(roleId,applicationId, moduleId);
        }


        public static List<Menu> GetMenusByApplicationAndModuleName(string appName, string modName)
        {
            return UserMgtAgent.GetMenusByApplicationAndModuleName(appName, modName);
        }

        public static Menu GetMenusByMenuId(int id)
        {
            return UserMgtAgent.GetMenusByMenuId(id);
        }

        public static List<Menu> GetMenus(string loginId, string appName, string modName)
        {
          return  UserMgtAgent.GetMenus(loginId, appName, modName);
        }

        public static List<Menu> GetParentMenusByModuleId(int ModuleId)
        {
            return UserMgtAgent.GetParentMenus(ModuleId);
        }


        #endregion

        #region Rights
        public static List<Right> GetAllRights()
        {
            return UserMgtAgent.GetAllRights();
        }

        public static List<Right> GetAllRightsMapedByRole(int roleId)
        {
            return UserMgtAgent.GetAllRightsMapedByRole(roleId);
        }

        public static List<Right> GetRightByRoleAndAppAndModule(int roleId, int appId, int moduleId)
        {
           return UserMgtAgent.GetRightByRoleAndAppAndModule(roleId, appId, moduleId);

        }

        #endregion

        #region Department
            public static List<Department> GetAllDepartment()
            {
                return UserMgtAgent.GetAllDepartment();
            }


        #endregion

        #region Zone
            public static List<Zone> GetZoneList()
            {
                return UserMgtAgent.GetZoneList();
            }

            public static void InsertEmpZone(string EmpId, List<Zone> zoneList)
            {
                UserMgtAgent.InsertEmpZone(EmpId, zoneList);
            }

            public static List<Zone> LoadZoneListByEmpID(string empId)
            {
                return UserMgtAgent.LoadZoneListByEmpID(empId);
            }

        #endregion

        #region Employee
        public static List<Employee> GetAllEmployee()
        {
           return UserMgtAgent.GetAllEmployee();
        }

        public static List<Employee> GetAllEmployeeWithPaging(int startRowIndex, int maxRow)
        {
           return  UserMgtAgent.GetAllEmployeeWithPaging(startRowIndex, maxRow);
        }

        public static List<Employee> GetSearchEmployeeWithPaging(Employee obj, int startRowIndex, int maxRow,out int intTotalRow)
        {
            return UserMgtAgent.GetSearchEmployeeWithPaging(obj, startRowIndex, maxRow, out intTotalRow);
        }


        public static Int32 GetTotalEmployeeCount()
        {
            return UserMgtAgent.GetTotalEmployeeCount(); 
        }
        #endregion

        #region Application
        public static List<Application> GetAllApplications()
        {
           return  UserMgtAgent.GetAllApplications();
        }

        public static Application GetApplicationById(int id)
        {
            return UserMgtAgent.GetApplicationById(id);
        }

        public static int CreateApplication(Application app)
        {
            return UserMgtAgent.CreateApplication(app);
        }

        public static int UpdateApplication(Application app)
        {
            return UserMgtAgent.UpdateApplication(app);
        }
        #endregion

        #region Module
        public static List<Module> GetAllModules()
        {
            return UserMgtAgent.GetAllModules();
        }

        public static List<Module> GetModulesByApp(int appId)
        {
            return UserMgtAgent.GetModulesByApplicationId(appId);
        }

        public static List<Menu> GetMenusByModuleId(int ModuleId)
        {
            return UserMgtAgent.GetMenusByModuleId(ModuleId);
        }

        public static Module GetModuleById(int id)
        {
            return UserMgtAgent.GetModuleById(id);
        }

        public static int CreateModule(Module module)
        {
            return UserMgtAgent.CreateModule(module);
        }

        public static int UpdateModule(Module module)
        {
            return UserMgtAgent.UpdateModule(module);
        }
        #endregion

        #region Dashboard
        public static List<Dashboard> GetZoneWiseUserList()
        {
            return UserMgtAgent.GetZoneWiseUserList();
        }

        #endregion

        #region User Menus
        public static void InsertUserMenus(UserMenu userMenu, List<Menu> menuList)
        {
            UserMgtAgent.InsertUserMenu(userMenu, menuList);
        }
        public static List<Menu> LoadUserMenuList(UserMenu userMenu)
        {
            return UserMgtAgent.LoadUserMenuList(userMenu);
        }
        public static List<UserMenu> GetAllUserMenuList(UserMenu userMenu)
        {
            return UserMgtAgent.GetAllUserMenuList(userMenu);
        }
        #endregion

        #region Validation


        public static IEnumerable<RuleViolation> GetUserRuleViolations(User user)
        {
            return UserMgtAgent.GetUserRuleViolations(user);
        }

        public static void ValidateUser(User user)
        {

        }

        #endregion
    }
}
