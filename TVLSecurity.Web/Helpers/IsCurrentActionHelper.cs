using System;
using System.Web.Mvc;

namespace Helpers
{
    public static class IsCurrentActionHelper
    {

        public static bool IsCurrentAction(this HtmlHelper helper, string actionName, string controllerName, System.Web.HttpContext context)
        {
            //string currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            //string currentActionName = (string)helper.ViewContext.RouteData.Values["action"];

            //if (currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase) && currentActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase))
            if(IsSelectControler(context.Request.Url.ToString(), controllerName))
                return true;

            return false;
        }

        public static bool IsSelectControler(string url, string controllerName)
        {
            string[] strContent = url.Split("/".ToCharArray());
            foreach (string strItem in strContent)
            {
                if (strItem.Trim() == controllerName.Trim())
                {
                    return true;
                }
            }

            return false;
        }

    }
}
