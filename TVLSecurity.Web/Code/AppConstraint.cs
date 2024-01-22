using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TVLSecurity.Web.Code
{
    public class AppConstraint
    {
        private static string strSystemInitializer= string.Empty;
        public static string SystemInitializer
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SystemInitializer"].ToString(); }
        }


        public static string ApplicationName
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["ApplicationName"].ToString(); }
        }

        public static string ModuleName
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["ModuleName"].ToString(); }
        }
    }
}