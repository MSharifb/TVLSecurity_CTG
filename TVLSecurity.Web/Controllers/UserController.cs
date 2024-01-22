using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TVLSecurity.Web.Models;
using Entity;
using System.Linq;
using System.Globalization;
using TVLSecurity.Web.Code;
using PrimaryBaseLibrary;

namespace TVLSecurity.Web.Controllers
{
    [NoCache]
    public class UserController : Controller
    {
        private UserManagement.Business.EmployeeBAL empBLL = new UserManagement.Business.EmployeeBAL();
        //
        // GET: /User/
        [NoCache]
        public ActionResult Index(int? page)
        {
            ViewData["PageTitle"] = "User";
            return View();
        }

        [NoCache]
        public ActionResult List(int? page)
        {
            ModelState.Clear();
            UserFormViewModel model = new UserFormViewModel();
            IList<User> userList = UserRepository.GetUserList().Where(c=> c.LoginId !=AppConstraint.SystemInitializer).OrderBy(x=>x.LoginId).ToList();

            model.SearchZoneList = new SelectList(UserRepository.GetZoneList(), "ZoneId", "ZoneName", "ZoneId");

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            if (userList.Count > 0)
            {
                //  model.UserList = userList.ToPagedList(currentPageIndex, 20);
                model.UserList = userList;
                model.NoDataFound = false;
                
            }
            else
            {
                model.NoDataFound = true;
                //return View("NoDataFound");
            }
            return PartialView("List", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult List(int? page, UserFormViewModel model)
        {

            ModelState.Clear();
            IList<User> userList = UserRepository.GetUserListByCraiteria(model.SearchCraiteria).Where(c => c.LoginId != AppConstraint.SystemInitializer).OrderBy(x => x.LoginId).ToList();

            model.SearchZoneList = new SelectList(UserRepository.GetZoneList(), "ZoneId", "ZoneName", model.SearchCraiteria.ZoneId.ToString());

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            if (userList.Count > 0)
            {

               // model.UserList = userList.ToPagedList(currentPageIndex, 20);
                model.NoDataFound = false;

            }
            else
            {
                model.NoDataFound = true;
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.NoDataFound);
            }

            return PartialView("List", model);
        }

        [NoCache]
        public ActionResult EmployeeList(int Id)
        {
            int? page=0;
            int totalRows = 0;
            UserFormViewModel model = new UserFormViewModel();
            model.DepartmentList = GetDropdownlistDepartment("");
            
            Employee objSearch = model.EmpSearchCraiteria;

            if (!string.IsNullOrEmpty(model.strSearchEmpID))
            {
                objSearch.EmpId = model.strSearchEmpID.Trim();
            }
            else
            {
                objSearch.EmpId = model.strSearchEmpID;
            }

            //if (!string.IsNullOrEmpty(model.strSearchEmpInitial))
            //{
            //    objSearch.EmployeeInitial = model.strSearchEmpInitial.Trim();
            //}
            //else
            //{
            //    objSearch.EmployeeInitial = model.strSearchEmpInitial;
            //}

            if (!string.IsNullOrEmpty(model.strSearchEmpName))
            {
                objSearch.EmpName = model.strSearchEmpName.Trim();
            }
            else
            {
                objSearch.EmpName = model.strSearchEmpName;
            }            
            
            objSearch.strDepartmentID = model.strSearchDepartmentID;
            objSearch.ZoneId = Id;
            IList<Employee> employeeList = empBLL.LoadSearchItemList(objSearch, 0, 9999, out totalRows);

            //IList<Employee> employeeList = UserRepository.GetSearchEmployeeWithPaging(objSearch, 0, 10, out totalRows);

            int currentPageIndex = page.HasValue ? page.Value : 0;
            if (employeeList.Count > 0)
            {               
                model.PageNumber = currentPageIndex + 1;
                model.PageSize = 11;
                model.EmployeeList = employeeList;               
                model.TotalRowCount = totalRows;  //UserRepository.GetTotalEmployeeCount();


                //ModelState.Clear();
                //return PartialView("EmployeeList", model);

            }
            //else
            //{
                //return View("NoDataFound");
            //}

            else
            {
                model.NoDataFound = true;
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.NoDataFound);
            }
            model.ZoneId = Id;
            ModelState.Clear();
            Response.Clear();
            return PartialView("EmployeeList", model);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public PartialViewResult EmployeeList(int? page, UserFormViewModel model, FormCollection coll)
        {

            int totalRows = 0;
            Employee objSearch = model.EmpSearchCraiteria;

            #region Searching Parameters

            if (!string.IsNullOrEmpty(model.strSearchEmpID))
            {
                objSearch.EmpId = model.strSearchEmpID.Trim();
            }
            else
            {
                objSearch.EmpId = model.strSearchEmpID;
            }

            //if (!string.IsNullOrEmpty(model.strSearchEmpInitial))
            //{
            //    objSearch.EmployeeInitial = model.strSearchEmpInitial.Trim();
            //}
            //else
            //{
            //    objSearch.EmployeeInitial = model.strSearchEmpInitial;
            //}

            if (!string.IsNullOrEmpty(model.strSearchEmpName))
            {
                objSearch.EmpName = model.strSearchEmpName.Trim();
            }
            else
            {
                objSearch.EmpName = model.strSearchEmpName;
            }
            objSearch.ZoneId =Convert.ToInt32(model.ZoneId);

            #endregion

            objSearch.strDepartmentID = model.strSearchDepartmentID;

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            IList<Employee> employeeList = empBLL.LoadSearchItemList(objSearch,currentPageIndex * 10, 10, out totalRows);

            //IList<Employee> employeeList1 = UserRepository.GetSearchEmployeeWithPaging(objSearch, currentPageIndex * 10, 10, out totalRows);

            model.DepartmentList = GetDropdownlistDepartment(model.strSearchDepartmentID);

            if (employeeList.Count > 0)
            {
                model.PageNumber = currentPageIndex + 1;
                model.PageSize = 11;

                model.TotalRowCount = totalRows;  //UserRepository.GetTotalEmployeeCount();
                model.EmployeeList = employeeList;
            }
            else
            {
                model.NoDataFound = true;
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.NoDataFound);
            }

            ModelState.Clear();
            Response.Clear();
            return PartialView("EmployeeList", model);

        }

        //
        // GET: /User/Details/5
        [NoCache]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create    
        [NoCache]
        public ActionResult Create()
        {
            UserFormViewModel model = new UserFormViewModel();
            model.RoleList = UserRepository.GetRoles();
            model.GroupList = GetDropdownlistItems(0);
            model.ZoneList = GetZoneDropdownlistItems(0, true);
            model.SelectedZoneList = UserRepository.GetZoneList();
            model.PopulateAppModSelectedList();

            #region Module List Items. Change Date:06-Nov-2014

            List<Module> modList = new List<Module>();
            Module module = new Module();
            module.ModuleTitle = "Select One";
            module.ModuleId = 0;
            modList.Add(module);
            model.ModuleList = new SelectList(modList, "ModuleId", "ModuleTitle");

            #endregion

            #region MenuList
            model.User.ApplicationId = model.ApplicationId = 0;
            model.ModuleId = model.ApplicationId = 0;

            model.PopulateSelectedMenuList();
            #endregion

            return PartialView("Create", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult Create(UserFormViewModel model)
        {
            try
            {

                if (ValidateUserInformation(model))
                {
                    model.User.CreatedBy = User.Identity.Name;

                    string hashPass = Common.getMd5Hash(model.User.Password);
                    model.User.Password = hashPass;
                    //Insert user data
                    int userId = UserRepository.InsertUserData(model.User);

                    //Insert Role Information for this user
                    //UserRepository.InsertUserRolesData(userId, model.RoleList, model.ApplicationId, model.ModuleId);

                    //Insert Menu Information for this user
                    UserMenu userMenu = new UserMenu();
                    userMenu.ApplicationId = model.User.ApplicationId;
                    userMenu.ModuleId = model.ModuleId;
                    userMenu.UserId = userId;

                    UserRepository.InsertUserMenus(userMenu, model.MenuList);


                    //Inser Zone
                    UserRepository.InsertEmpZone(model.User.EmpId, model.SelectedZoneList);                   

                    //set Model is valid
                    model.IsValidModel = true;

                    //set message
                    model.Message = Messages.GetMessage(Messages.MessageIdEnum.SavedSuccessFully);

                    //populate group info
                    model.GroupList = GetDropdownlistItems(model.User.GroupId);
                    model.ZoneList = GetZoneDropdownlistItems(1,true);
                    model.SelectedZoneList = UserRepository.GetZoneList();
                    //render view 
                    model.User = new User();
                    model.ConfirmPassword = string.Empty;
                    ModelState.Clear();

                }
                else
                {

                    //model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
                    //model.IsValidModel = false;
                    throw new Exception();


                }

            }
            catch (Exception ex)
            {
                model.IsValidModel = false;
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
                //if not valid
                model.GroupList = GetDropdownlistItems(model.User.GroupId);
                model.ZoneList = GetZoneDropdownlistItems(model.User.ZoneId,false);
                model.SelectedZoneList = UserRepository.GetZoneList();

            }

            //populate application and module list
            model.PopulateAppModSelectedList();

            #region MenuList
            model.User.ApplicationId = model.ApplicationId = 0;
            model.ModuleId = model.ApplicationId = 0;

            model.PopulateSelectedMenuList();
            #endregion


            #region Module List Items. Including Date:06-Nov-2014

            if (model.ApplicationId == 0)
            {
                List<Module> modList = new List<Module>();
                Module module = new Module();
                module.ModuleTitle = "Select One";
                module.ModuleId = 0;
                modList.Add(module);
                model.ModuleList = new SelectList(modList, "ModuleId", "ModuleTitle");
            }
            #endregion

            return PartialView("Create", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult GetRoleList(UserFormViewModel model)
        {
            try
            {


                Role role = new Role();
                role.ApplicationId = model.ApplicationId;
                role.ModuleId = model.ModuleId;
                role.UserId = model.User.UserId;

                model.ApplicationId = model.User.ApplicationId;
                //set password field
                model.ConfirmPassword = model.User.Password;

                //Get Role List by searching craiteria(Application/Module)
                model.RoleList = UserRepository.GetRoleListByCraiteria(role);

                //Populate GroupList
                model.GroupList = GetDropdownlistItems(model.User.GroupId);

                //populate application and module list
                model.PopulateAppModSelectedList();

                //Get ZoneList
                model.ZoneList = GetZoneDropdownlistItems(0, true);
                model.SelectedZoneList = UserRepository.GetZoneList();

                #region Module List Items. Including Date:06-Nov-2014

                if (model.ApplicationId == 0)
                {
                    List<Module> modList = new List<Module>();
                    Module module = new Module();
                    module.ModuleTitle = "Select One";
                    module.ModuleId = 0;
                    modList.Add(module);
                    model.ModuleList = new SelectList(modList, "ModuleId", "ModuleTitle");
                }

                #endregion

                //set Model is valid
                model.IsValidModel = true;
                ModelState.Clear();




            }
            catch
            {
                model.IsValidModel = false;
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.NotAbleToLoad);
            }

            if (model.User.UserId <= 0)
            {
                return PartialView("Create", model);
            }
            else
            {
                model.ZoneList = GetZoneDropdownlistItems(Convert.ToInt32(model.User.ZoneId), false);
                model.GetZoneList = UserRepository.LoadZoneListByEmpID(model.User.EmpId);

                return PartialView("Edit", model);
            }
        }

        [NoCache]
        public ActionResult Delete(int id)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    UserRepository.DeleteUserData(id);

                }

                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /User/Create


        //
        // GET: /User/Edit/5
        [NoCache]
        public ActionResult Edit(int id)
        {
            UserFormViewModel model = new UserFormViewModel();
            //Get user info
            model.User = UserRepository.GetUser(id);

            //set password field
            model.ConfirmPassword = model.User.Password;

            model.OldPassword = model.User.Password;

            //set ApplicationId
            model.ApplicationId = model.User.ApplicationId;

            //get role
            model.RoleList = UserRepository.GetRolesByUser(id);

            //get group
            model.GroupList = GetDropdownlistItems(model.User.GroupId);

            //Populate Application and Module Selected List
            model.PopulateAppModSelectedList();

            //Populate Zone
            model.ZoneList = GetZoneDropdownlistItems(Convert.ToInt32(model.User.ZoneId),false);
            model.SelectedZoneList = UserRepository.GetZoneList();
            model.GetZoneList = UserRepository.LoadZoneListByEmpID(model.User.EmpId);

            //Get User Menus list     
            UserMenu userMenu = new UserMenu();
            userMenu.ApplicationId = model.User.ApplicationId;
            userMenu.UserId = id;
            userMenu.ModuleId = model.ModuleId;

            model.MenuList = UserRepository.LoadUserMenuList(userMenu);

            #region Module List Items. Including Date:06-Nov-2014

            if (model.ApplicationId == 0)
            {
                List<Module> modList = new List<Module>();
                Module module = new Module();
                module.ModuleTitle = "Select One";
                module.ModuleId = 0;
                modList.Add(module);
                model.ModuleList = new SelectList(modList, "ModuleId", "ModuleTitle");
            }
            #endregion
           
            //render view
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult Edit(UserFormViewModel model)
        {
            try
            {
                if (ValidateUserInformation(model))
                {
                    // if all information is valid                    
                    model.User.UpdatedBy = User.Identity.Name;
                    if (model.User.Password != model.OldPassword)
                    {
                      string hashPass =  TVLSecurity.Web.Code.Common.getMd5Hash(model.User.Password);
                      model.User.Password = hashPass;
                    }
                    //Update user information
                    UserRepository.UpdateUserData(model.User);

                    //update role information
                    //UserRepository.InsertUserRolesData(model.User.UserId, model.RoleList, model.ApplicationId, model.ModuleId);

                    //Update Zone
                    UserRepository.InsertEmpZone(model.User.EmpId, model.GetZoneList);

                    //Insert Menu Information for this user
                    UserMenu userMenu = new UserMenu();
                    userMenu.ApplicationId = model.User.ApplicationId;
                    userMenu.ModuleId = model.ModuleId;
                    userMenu.UserId = model.User.UserId;

                    UserRepository.InsertUserMenus(userMenu, model.MenuList);


                    //get grouplist
                    model.GroupList = GetDropdownlistItems(model.User.GroupId);

                    //set ApplicationId
                    model.ApplicationId = model.User.ApplicationId;

                    //Populate Application and Module Selected List
                    model.PopulateAppModSelectedList();

                    //Populate Zone
                    model.ZoneList = GetZoneDropdownlistItems(Convert.ToInt32(model.User.ZoneId),false);
                    model.SelectedZoneList = UserRepository.GetZoneList();
                    model.GetZoneList = UserRepository.LoadZoneListByEmpID(model.User.EmpId);

                    #region Module List Items. Including Date:06-Nov-2014

                    if (model.ApplicationId == 0)
                    {
                        List<Module> modList = new List<Module>();
                        Module module = new Module();
                        module.ModuleTitle = "Select One";
                        module.ModuleId = 0;
                        modList.Add(module);
                        model.ModuleList = new SelectList(modList, "ModuleId", "ModuleTitle");
                    }
                    #endregion

                    //Get User Menus list     
                    userMenu.ApplicationId = model.User.ApplicationId;
                    userMenu.UserId = model.User.UserId;
                    userMenu.ModuleId = model.ModuleId;
                    model.MenuList = UserRepository.LoadUserMenuList(userMenu);


                    //set model is valid
                    model.IsValidModel = true;
                    model.Message = Messages.GetMessage(Messages.MessageIdEnum.UpdatedSuccessFully);
                    //render view
                    return PartialView("Edit", model);

                }
                else
                {

                    //model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
                    //model.IsValidModel = false;
                    throw new Exception();


                }

            }
            catch (Exception ex)
            {
                model.IsValidModel = false;
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
                //if not valid
                model.PopulateAppModSelectedList();
                model.GroupList = GetDropdownlistItems(model.User.GroupId);
                model.ZoneList = GetZoneDropdownlistItems(Convert.ToInt32(model.User.ZoneId), false);
                model.GetZoneList = UserRepository.LoadZoneListByEmpID(model.User.EmpId);
            }

            //populate application and module list
            model.PopulateAppModSelectedList();

            #region Module List Items. Including Date:06-Nov-2014

            if (model.ApplicationId == 0)
            {
                List<Module> modList = new List<Module>();
                Module module = new Module();
                module.ModuleTitle = "Select One";
                module.ModuleId = 0;
                modList.Add(module);
                model.ModuleList = new SelectList(modList, "ModuleId", "ModuleTitle");
            }
            #endregion

            #region MenuList
            model.User.ApplicationId = model.ApplicationId = 0;
            model.ModuleId = model.ApplicationId = 0;

            model.PopulateSelectedMenuList();
            #endregion

            return PartialView("Edit", model);
        }

        [NoCache]
        private List<SelectListItem> GetDropdownlistItems(int? groupId)
        {

            List<SelectListItem> selectList = new List<SelectListItem>();
            List<UserGroup> groupList = UserRepository.GetGroupList();

            foreach (UserGroup group in groupList)
            {
                SelectListItem item = new SelectListItem { Text = group.GroupName, Value = group.GroupId.ToString() };
                if (groupId == group.GroupId)
                {
                    item.Selected = true;
                }
                selectList.Add(item);
            }
            return selectList;

        }

        [NoCache]
        private List<SelectListItem> GetZoneDropdownlistItems(int zoneId, bool isCreate)
        {

            List<SelectListItem> selectList = new List<SelectListItem>();
            List<Zone> groupList = UserRepository.GetZoneList();

            foreach (Zone zone in groupList)
            {
                SelectListItem item = new SelectListItem { Text = zone.ZoneName, Value = zone.ZoneId.ToString() };
                if (zoneId == zone.ZoneId && isCreate==false)
                {
                    item.Selected = true;
                }
                selectList.Add(item);
            }
            return selectList;

        }
        private List<SelectListItem> GetDropdownlistDepartment(string deptId)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            List<Department> departmentList = UserRepository.GetAllDepartment();

            foreach (Department dept in departmentList)
            {
                SelectListItem item = new SelectListItem { Text = dept.strDepartment, Value = dept.strDepartmentID.ToString() };
                if (deptId == dept.strDepartmentID)
                {
                    item.Selected = true;
                }
                selectList.Add(item);
            }
            return selectList;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public JsonResult GetModuleList(UserFormViewModel model)
        {

            return Json(model.GetModuleListByApp(model.User.ApplicationId));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public PartialViewResult GetMenuList(UserFormViewModel model)
        {


            try
            {
                ModelState.Clear();
                model.ApplicationId = model.User.ApplicationId = model.User.ApplicationId == 0 ? 0 : model.User.ApplicationId;
                model.ModuleId = model.ModuleId = model.ModuleId == 0 ? 0 : model.ModuleId;

                //Populate Menulist
                //model.PopulateSelectedMenuList();
                UserMenu userMenu = new UserMenu();
                userMenu.ApplicationId = model.User.ApplicationId;
                userMenu.UserId = model.User.UserId;
                userMenu.ModuleId = model.ModuleId;

                model.MenuList = UserRepository.LoadUserMenuList(userMenu);

                //Populate Right List by application and Module
                model.PopulateRightList();
                //Populate GroupList
                model.GroupList = GetDropdownlistItems(model.User.GroupId);

                //populate application and module list
                model.PopulateAppModSelectedList();

                //Get ZoneList
                model.ZoneList = GetZoneDropdownlistItems(0, true);
                model.SelectedZoneList = UserRepository.GetZoneList();

                #region Module List Items. Including Date:06-Nov-2014

                if (model.ApplicationId == 0)
                {
                    List<Module> modList = new List<Module>();
                    Module module = new Module();
                    module.ModuleTitle = "Select One";
                    module.ModuleId = 0;
                    modList.Add(module);
                    model.ModuleList = new SelectList(modList, "ModuleId", "ModuleTitle");
                }

                #endregion

                //set Model is valid
                model.IsValidModel = true;

            }
            catch
            {
                model.IsValidModel = false;
                model.Message = Messages.GetMessage(Messages.MessageIdEnum.CouldntSave);
            }


            if (model.User.UserId > 0)
            {

                return PartialView("MenuListForEdit", model);
            }
            else
            {
                return PartialView("MenuList", model);
            }
        }


        [NoCache]
        private bool ValidateUserInformation(UserFormViewModel model)
        {
            ModelState.Clear();
            //ModelState.Remove("Model.User.GroupId");

            //if (String.IsNullOrEmpty(model.User.FirstName))
            //{
            //    ModelState.AddModelError("Model.User.FirstName", "The First Name field required.");
            //}

            //if (String.IsNullOrEmpty(model.User.LastName))
            //{
            //    ModelState.AddModelError("Model.User.LastName", "The Name field required");
            //}

            if (string.IsNullOrEmpty(model.User.LoginId))
            {
                ModelState.AddModelError("Model.User.LoginId", " The Login Id is a required field.");
            }
            else
            {

                //--[Block by shaiful dated on 28 sep 2011, advice by Mr. Jahangir, System Analyst, TVL] 
                //System.Text.RegularExpressions.Regex regLoginId = new System.Text.RegularExpressions.Regex(@"^[A-Za-z](?=[A-Za-z0-9_@.-]{3,31}$)[a-zA-Z0-9_@.-]*\.?[a-zA-Z0-9_@.-]*$");
                //if (regLoginId.IsMatch(model.User.LoginId.Trim()))
                //{
                    int userId = UserRepository.GetUserByLoginId(model.User.LoginId.Trim()).UserId;
                    if (userId > 0 && userId != model.User.UserId)
                    {
                        ModelState.AddModelError("Model.User.LoginId", "The Login Id already exist.");
                    }
                //}
                //else
                //{
                    //ModelState.AddModelError("Model.User.LoginId", "The Login Id is not valid.Use 4 to 32 characters and start with a letter. You may use letters, numbers, underscores, and one dot (.)");
                    //ModelState.AddModelError("Model.User.LoginId", "The Login Id is not valid.Use 4 to 32 characters and start with a letter. You may use letters, numbers, symbols(-,@,_) and dot(.)");
                //}

            }

            if (model.User.ApplicationId <= 0)
            {
                ModelState.AddModelError("Model.User.ApplicationId", "The Application is a required field.");
            }

            if (model.User.ZoneId <= 0)
            {
                ModelState.AddModelError("Model.User.ZoneId", "The Zone is a required field.");
            }



            if (model.User.Password == null || model.User.Password.Length < 6)
            {
                ModelState.AddModelError("Model.User.Password",
                    String.Format(CultureInfo.CurrentCulture,
                         "Password must be {0} or more characters.",
                        6));
            }

            if (!String.Equals(model.User.Password, model.ConfirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("Model.ConfirmPassword", "Password dose not match.");
            }

            if (!string.IsNullOrEmpty(model.User.EmailAddress))
            {
                System.Text.RegularExpressions.Regex regEmail = new System.Text.RegularExpressions.Regex(@"^([\w-_]+\.)*[\w-_]+@([\w-_]+\.)*[\w-_]+\.[\w-_]+$");
                if (!regEmail.IsMatch(model.User.EmailAddress.Trim()))
                {
                    ModelState.AddModelError("Model.User.EmailAddress", "Invalid email address.");
                }
            }

            if (!string.IsNullOrEmpty(model.User.EmpId))
            {

                IList<Employee> employeeList = UserRepository.GetAllEmployee().Where(c => c.EmpId == model.User.EmpId.Trim()).ToList();

                if (employeeList.Count <= 0)
                {
                    model.User.LastName = "";
                    model.User.EmailAddress = "";
                    ModelState.AddModelError("Model.User.EmpId", "Invalid Employee ID.");
                }
                else
                {
                    model.User.LastName = employeeList[0].EmpName;
                    model.User.EmailAddress = employeeList[0].strEmail;
                }

                List<User> userList = UserRepository.GetUserList().Where(c => c.EmpId == model.User.EmpId.Trim() && c.UserId != model.User.UserId).ToList();
                if (userList.Count > 0)
                {
                    ModelState.AddModelError("Model.User.EmpId", "Employee ID already exist.");
                }


            }
            else
            {
                model.User.LastName = "";
                model.User.EmailAddress = "";
                ModelState.AddModelError("Model.User.EmpId", " The Employee ID is a required field.");               
            }

            return ModelState.IsValid;
        }

    }


}