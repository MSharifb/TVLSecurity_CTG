using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using TVLSecurity.Web.Models;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;

namespace TVLSecurity.Web.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {


        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.

        public AccountController()
            : this(null, null)
        {
        }

        // This constructor is not used by the MVC framework but is instead provided for ease
        // of unit testing this type. See the comments at the end of this file for more
        // information.
        public AccountController(IFormsAuthentication formsAuth, IMembershipService service)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationService();
            MembershipService = service ?? new AccountMembershipService();
        }

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }

        public IMembershipService MembershipService
        {
            get;
            private set;
        }

        public ActionResult LogOn()
        {

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
       
        public ActionResult LogOn(string userName, string password, bool rememberMe, string returnUrl)
        {

            if (!ValidateLogOn(userName, password))
            {
                return View();
            }

            FormsAuth.SignIn(userName, rememberMe);

            //string permissionList = "Newton,Sarker";

            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(30), true, permissionList, FormsAuthentication.FormsCookiePath);
            //string hash = FormsAuthentication.Encrypt(ticket);
            //HttpCookie cookie = new HttpCookie("tvlAuthCookie",hash);
            //if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            //Response.Cookies.Add(cookie);

            //Session["testSession"] = "NewtonS";

            //NameValueCollection headers = Request.Headers;
            //Type t = headers.GetType();
            //PropertyInfo p = t.GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            //p.SetValue(headers, false, null);
            //headers.Add("tvlAuthHeader", permissionList);

            //Response.AppendHeader("tvlAuthHeader", permissionList);
            //Response.AddHeader("tvlAuthHeader", permissionList);

            if (!String.IsNullOrEmpty(returnUrl))
            {
                if (!String.IsNullOrEmpty(returnUrl.Replace("Account", "")))
                {
                    return Redirect(returnUrl);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            Session.Clear();                    
            HttpContext.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
            FormsAuth.SignOut();            

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            return View();
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Register(string userName, string email, string password, string confirmPassword)
        //{

        //    ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

        //    if (ValidateRegistration(userName, email, password, confirmPassword))
        //    {
        //        // Attempt to register the user
        //        MembershipCreateStatus createStatus = MembershipService.CreateUser(userName, password, email);

        //        if (createStatus == MembershipCreateStatus.Success)
        //        {
        //            FormsAuth.SignIn(userName, false /* createPersistentCookie */);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View();
        //}

        [Authorize]
        public ActionResult ChangePassword()
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            ChangePasswordModel model = new ChangePasswordModel();
            model.OldPassword = "";
            return View(model);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (!ValidateChangePassword(model.OldPassword, model.NewPassword, model.ConfirmPassword))
            {
                return View(model);
            }

            try
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                    //ModelState.AddModelError("currentPassword", "The current password is incorrect.");
                    return View(model);
                }
            }
            catch
            {
                ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");                
                
                return View(model);
            }
        }

        public ActionResult ChangePasswordSuccess()
        {

            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        #region Validation Methods

        private bool ValidateChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            ModelState.Clear();

            if (String.IsNullOrEmpty(currentPassword))
            {
                ModelState.AddModelError("currentPassword", "You must specify a current password.");
            }
            if (newPassword == null || newPassword.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("newPassword",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a new password of {0} or more characters.",
                         MembershipService.MinPasswordLength));
            }

            if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateLogOn(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a login id.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }
            if (!MembershipService.ValidateUser(userName, password))
            {
                ModelState.AddModelError("_FORM", "The login id or password provided is incorrect.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a login id.");
            }
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "You must specify an email address.");
            }
            if (password == null || password.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a password of {0} or more characters.",
                         MembershipService.MinPasswordLength));
            }
            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }
            return ModelState.IsValid;
        }

        
        #endregion
        //}

        // The FormsAuthentication type is sealed and contains static members, so it is difficult to
        // unit test code that calls its members. The interface and helper class below demonstrate
        // how to create an abstract wrapper around such a type in order to make the AccountController
        // code unit testable.

        public interface IFormsAuthentication
        {
            void SignIn(string userName, bool createPersistentCookie);
            void SignOut();
        }

        public class FormsAuthenticationService : IFormsAuthentication
        {
            public void SignIn(string userName, bool createPersistentCookie)
            {
                FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
            }
            public void SignOut()
            {
                FormsAuthentication.SignOut();
            }
        }

        public interface IMembershipService
        {
            int MinPasswordLength { get; }

            bool ValidateUser(string userName, string password);
            MembershipCreateStatus CreateUser(string userName, string password, string email);
            bool ChangePassword(string userName, string oldPassword, string newPassword);
        }

        public class AccountMembershipService : IMembershipService
        {
            private CustomMembershipProvider _provider;

            public AccountMembershipService()
                : this(null)
            {
            }

            public AccountMembershipService(CustomMembershipProvider provider)
            {
                _provider = provider ?? new CustomMembershipProvider();
            }

            public int MinPasswordLength
            {
                get
                {
                    return _provider.MinRequiredPasswordLength;
                }
            }

            public bool ValidateUser(string userName, string password)
            {
                return _provider.ValidateUser(userName, password);
            }

            public MembershipCreateStatus CreateUser(string userName, string password, string email)
            {
                MembershipCreateStatus status;
                _provider.CreateUser(userName, password, email, null, null, true, null, out status);
                return status;
            }

            public bool ChangePassword(string userName, string oldPassword, string newPassword)
            {
                return CustomMemberShip.Provider.ChangePassword(userName, oldPassword, newPassword);
                //MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                //return currentUser.ChangePassword(oldPassword, newPassword);
            }
        }
    }
}