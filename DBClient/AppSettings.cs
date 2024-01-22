using System;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;

namespace DBClient
{
    public static class AppSettings
    {
        #region Private Members
        private static System.String strConnection;
        private static System.String strProvider;
        private static System.String strAppName;
        private static System.String strPrefix;
        #endregion

        #region Public Property
        public static string DBConnectionString
        {
            get
            {
                return ConfigurationSettings.AppSettings["CnnStr"].ToString();
            }            
        }

        public static string DBProvider
        {
            get
            {
                return ConfigurationSettings.AppSettings["Provider"].ToString();
            }            
        }

        public static string ApplicationName
        {

            get { return ConfigurationSettings.AppSettings["ApplicationName"].ToString(); }
        }

        public static string OutputPramPrefix
        {            
            get { return ConfigurationSettings.AppSettings["OutputParamPrefix"].ToString(); }
        }


        public static string GetPackageName()
        {
            return "pos.";
            //   return ConfigurationSettings.AppSettings["GetPOSSchema"].ToString();
        }

        public static int ErrorCode = -10000000;
        #endregion
    }
}
