using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TVLSecurity.Business;

namespace TVLSecurity.Web
{
    public class Permission
    {
        public static Entity.Menu objMenu;

        public enum MenuNamesId
        {
            Setup = 1,
            LeaveYear,
            OfficeTime,
            LeaveType,
            LeaveRule,
            AssignLeaveRule,
            WeekendAndHoliday,
            WeekendAndHolidayRule,
            AssignWeekendAndHolidayRule,
            ApprovalPath,
            LeaveOpening,
            LeaveEntitlement,
            LeaveEncasment,
            OnlineLeaveApplication,
            ApproveApplicationList,
            SubmitedApplicationList,
            RejectedApplicationList,
            SearchApplication,
            Administration,
            ApplyOfflineApplication,
            ApprovalAuthorityAttendence,
            SupervisorSetup,
            AlternateApprovalProcess,
            LeaveInfo,
            LeaveStatus,            
            EmployeeWiseApprovalPath,
            OfflineLeaveApplication,
            SystemInitialization,
            Initialization,
            Synchronize


        }

        public enum MenuOperation
        {
            Add = 1,
            Edit,
            Delete,
            Cancel,
            Print
        }

        public enum RightNamesId
        {
            SetApprover = 1,
           
        }

        public static bool IsMenuOperationPermited(MenuNamesId id, MenuOperation opt)
        {
            if (objMenu == null || String.Compare(objMenu.MenuName, id.ToString().Trim()) != 0)
            {
                objMenu = UserMgtAgent.GetMenuByMenuNameAndLoginId(HttpContext.Current.User.Identity.Name, id.ToString());
            }

            if (opt == MenuOperation.Add)
            {
                return objMenu.IsAddAssigned;
            }
            else if (opt == MenuOperation.Edit)
            {
                return objMenu.IsEditAssigned;
            }
            else if (opt == MenuOperation.Delete)
            {
                return objMenu.IsDeleteAssigned;
            }
            else if (opt == MenuOperation.Cancel)
            {
                return objMenu.IsCancelAssigned;
            }
            else if (opt == MenuOperation.Print)
            {
                return objMenu.IsPrintAssigned;
            }
            else
            {
                return false;
            }

        }

        //public static bool IsMenuOperationPermited(MenuNamesId id, MenuOperation opt, string loginId)
        //{
            
        //}

        public static bool IsRightPermited(string loginId, RightNamesId id)
        {
            Entity.Right right = UserMgtAgent.GetRightByLoginIdAndRightName(loginId.Trim(), id.ToString());
           return right.IsAssignedRight;
        }
    }
}