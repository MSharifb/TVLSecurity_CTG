using System;
using System.Collections.Generic;
using System.ServiceModel;
using Entity;

namespace UserManagementService
{
    // NOTE: If you change the interface name "IUserManagementService" here, you must also update the reference to "IUserManagementService" in Web.config.
    [ServiceContract]
    public interface IUserManagementService
    {


        #region User Info Operation
        [OperationContract]
        Int32 InsertUserData(User user);

        [OperationContract]
        void DeleteUserData(int id);

        [OperationContract]
        int UpdateUserData(User user);

        [OperationContract]
        Int32 InsertUserRoles(int userId, List<Role> roleList, int applicationId, int moduleId);

        [OperationContract]
        List<User> GetUserList();

        [OperationContract]
        List<User> GetUserListByCraiteria(User user);

        //[OperationContract]
        //Entity.UserRoleInfo GetUserRole(int id);

        [OperationContract]
        List<UserRoleInfo> GetUserRole(int id);

        [OperationContract]
        Entity.User GetUser(int id);

        [OperationContract]
        User GetUserByLoginId(string loginId);

        #endregion

        #region GroupOperation
        [OperationContract]
        Int32 CreateGroup(UserGroup item, List<Role> roleList);

        [OperationContract]
        Int32 UpdateGroup(UserGroup item, List<Role> roleList);

        [OperationContract]
        Int32 DeleteGroup(int id);

        [OperationContract]
        List<UserGroup> GetGroupList();

        [OperationContract]
        UserGroup GetGroupById(int id);
        #endregion

        #region Role Operation

        [OperationContract]
        List<Role> GetRolesList();

        [OperationContract]
        List<Role> GetRolesListByCraiteria(Role role);

        [OperationContract]
        List<RoleGroup> GetRoleGroups();

        [OperationContract]
        List<Role> GetRolesListByUser(int userId);

        [OperationContract]
        List<Role> GetRolesListByUserGroup(int groupId);

        [OperationContract]
        Int32 InsertRolesMenusRight(Role role, List<Menu> menuList, List<Right> rightList);

        [OperationContract]
        Int32 UpdateRolesMenusRight(Role role, List<Menu> menuList, List<Right> rightList);

        [OperationContract]
        Role GetRoleById(int id);

        [OperationContract]
        Int32 CreateRoleGroup(RoleGroup item);

        [OperationContract]
        Int32 UpdateRoleGroup(RoleGroup item);

        [OperationContract]
        Int32 DeleteRoleGroup(int Id);

        [OperationContract]
        RoleGroup GetRoleGroupById(int id);



        #endregion

        #region Menu Operation
        [OperationContract]
        int InsertMenuData(Menu menu);

        [OperationContract]
        int UpdateMenuData(Menu menu);

        [OperationContract]
        int DeleteMenuData(int id);


        [OperationContract]
        List<Menu> GetAllMenus();

        [OperationContract]
        List<Menu> GetMenusByParent(int parentId);

        [OperationContract]
        List<Menu> GetMenuListByRoleId(int roleId);

        [OperationContract]
        List<Menu> GetParentMenuListByLoginId(string loginId);

        [OperationContract]
        List<Menu> GetChildMenuListByLoginAndParentId(string loginId, int parentId);

        [OperationContract]
        Menu GetMenuByMenuNameAndLoginId(string loginId, string MenuName);

        [OperationContract]
        Menu GetMenuByMenuName(string MenuName);

        [OperationContract]
        List<Menu> GetMenusByApplicationAndModuleId(int roleId, int applicationId, int moduleId);

        [OperationContract]
        List<Menu> GetMenusByApplicationAndModuleName(string appName, string modName);

        [OperationContract]
        Menu GetMenuByMenuId(int id);

        [OperationContract]
        List<Menu> GetMenus(string loginId, string appName, string moduleName);

        [OperationContract]
        List<Menu> GetParentMenus(int ModuleId);

        #endregion

        #region Right Operation

        [OperationContract]
        List<Right> GetAllRights();

        [OperationContract]
        List<Right> GetAllRightsMapedByRole(int roleId);

        [OperationContract]
        Right GetRightByLoginIdAndRightName(string loginId, string rightName);

        [OperationContract]
        List<Right> GetRightsByRoleAndAppAndModule(int roleId, int appId, int moduleId);

        #endregion

        #region Validation

        //[OperationContract]
        //void ValidateUser(User user);

        [OperationContract]
        bool ValidateUser(string userId, string password);

        #endregion

        #region Department
        [OperationContract]
        List<Department> GetAllDepartment();
        #endregion

        #region Zone
        [OperationContract]
        List<Zone> GetZoneList();

        [OperationContract]
        Int32 InsertEmpZone(string EmpId, List<Zone> roleList);

        [OperationContract]
        List<Zone> LoadZoneListByEmpID(string empId);


        #endregion

        #region Employee

        [OperationContract]
        List<Employee> GetEmployeesByApplicationId(int applicationId);

        [OperationContract]
        List<Employee> GetAllEmployee();

        [OperationContract]
        List<Employee> GetAllEmployeeWithPaging(int startRowIndex, int maxRows);

        [OperationContract]
        List<Employee> GetSearchEmployeeWithPaging(Employee obj,int startRowIndex, int maxRows, out int intTotalRows);

        [OperationContract]
        Int32 GetTotalEmployeeCount();

        #endregion

        #region "Application"
        [OperationContract]
        List<Application> GetAllApplications();

        [OperationContract]
        Application GetApplicationById(int id);

        [OperationContract]
        int InsertApplicationData(Entity.Application app);

        [OperationContract]
        int UpdateApplicationData(Entity.Application app);

        [OperationContract]
        int DeleteApplicationData(int id);
        #endregion

        #region "Module"
        [OperationContract]
        List<Module> GetAllModules();

        [OperationContract]
        List<Module> GetModulesByApplicationId(int appId);

        [OperationContract]
        Module GetModuleById(int id);

        [OperationContract]
        int InsertModuleData(Entity.Module module);

        [OperationContract]
        int UpdateModuleData(Entity.Module module);

        [OperationContract]
        int DeleteModuleData(int id);
        #endregion

        #region Dashboard

        [OperationContract]
        List<Dashboard> GetZoneWiseUserList();

        #endregion

        #region UserMenu
        [OperationContract]
        Int32 InsertUserMenus(UserMenu userMenu, List<Menu> menuList);

        [OperationContract]
        Int32 UpdateUserMenus(UserMenu userMenu, List<Menu> menuList);

        [OperationContract]
        List<Menu> LoadUserMenuList(UserMenu userMenu);

        [OperationContract]
        List<UserMenu> GetAllUserMenuList(UserMenu item);

        #endregion

        // TODO: Add your service operations here
    }



}
