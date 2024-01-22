using System;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Collections.Specialized;
using System.Web.SessionState;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using Entity;
using TVLSecurity.Business;

namespace TVLSecurity.Web
{
    public sealed class CustomSessionStateStoreProvider : SessionStateStoreProviderBase
    {

        private SessionStateSection pConfig = null;
        //private ConnectionStringsSection pConnStr = null;
        //private string connectionString;
        //private ConnectionStringSettings pConnectionStringSettings;
        private string eventSource = "CustomSessionStateStore";
        private string eventLog = "Application";
        private string exceptionMessage = "An exception occurred. Please contact your administrator.";
        private string pApplicationName;
        //private int intApplicationId;

        //
        // If False, exceptions are thrown to the caller. If True,
        // exceptions are written to the event log.
        //

        private bool pWriteExceptionsToEventLog = false;

        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }

        //
        // The ApplicationName property is used to differentiate sessions
        // in the data source by application.
        //

        public string ApplicationName
        {
            get { return pApplicationName; }
        }

        //public int ApplicationId
        //{
        //    get { return intApplicationId; }
        //}

        //
        // ProviderBase members
        //

        public override void Initialize(string name, NameValueCollection config)
        {

            //
            // Initialize values from web.config.
            //

            if (config == null) throw new ArgumentNullException("config");

            if (name == null || name.Length == 0) name = "CustomSessionStateStoreProvider";

            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Sql Server Custom Session State Store provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);

            //
            // Initialize the ApplicationName property.
            //

            pApplicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;

            //
            // Get <sessionState> configuration element.
            //

            System.Configuration.Configuration cfg = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            //pConnStr = (ConnectionStringsSection)cfg.GetSection("connectionStrings");
            pConfig = (SessionStateSection)cfg.GetSection("system.web/sessionState");

            //
            // Initialize OdbcConnection.
            //

            //pConnectionStringSettings = ConfigurationManager.ConnectionStrings["COMPANY"];

            //if (pConnectionStringSettings == null || string.IsNullOrEmpty(pConnectionStringSettings.ConnectionString.Trim()))
            //{

            //    throw new HttpException("Connection string cannot be blank.");
            //}

            //connectionString = pConnectionStringSettings.ConnectionString;

            //
            // Initialize WriteExceptionsToEventLog
            //

            pWriteExceptionsToEventLog = false;

            if ((config["writeExceptionsToEventLog"] != null))
            {
                if (config["writeExceptionsToEventLog"].ToUpper() == "TRUE") pWriteExceptionsToEventLog = true;

            }
        }

        //
        // SessionStateStoreProviderBase members
        //

        public override void Dispose()
        {

        }


        //
        // SessionStateProviderBase.SetItemExpireCallback
        //

        public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
        {
            return false;
        }


        public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
        {

            
            string sessItems = Serialize((SessionStateItemCollection)item.Items);
            try
            {
                if (newItem)
                {


                    //CustomSession delSession = new CustomSession();

                    ////Delete session
                    //delSession.SESSIONID = id;
                    //delSession.ApplicationId = 1;
                    //delSession.EXPIRES = DateTime.Now;
                    //delSession.ApplicationName = pApplicationName;
                    //delSession.OperationMode = "IL";
                    //CustomSessionBAL.GetInstance().DeleteData(delSession);



                    //Add session

                    CustomSession newSession = new CustomSession();
                    newSession.SESSIONID = id;
                    newSession.ApplicationName = pApplicationName;
                    newSession.CREATED = DateTime.Now;
                    newSession.EXPIRES = DateTime.Now.AddMinutes((double)item.Timeout);
                    newSession.LOCKDATE = DateTime.Now;
                    newSession.LOCKID = 0;
                    newSession.LOCKED = 1;
                    newSession.SESSIONITEMS = sessItems;
                    newSession.FLAGS = 0;
                    newSession.TIMEOUT = item.Timeout;

                    CustomSessionBAL.GetInstance().InsertData(newSession);

                }
                else
                {


                    CustomSession upSession = new CustomSession();
                    upSession.EXPIRES = DateTime.Now.AddMinutes((double)item.Timeout);
                    upSession.SESSIONITEMS = sessItems;
                    upSession.LOCKED = 1;
                    upSession.SESSIONID = id;
                    upSession.ApplicationName = pApplicationName;
                    upSession.LOCKID = Convert.ToInt32(lockId);
                    upSession.OperationMode = "UR";
                    CustomSessionBAL.GetInstance().UpdateData(upSession);




                }



            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "SetAndReleaseItemExclusive");
                    throw new Exception(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {


            }
        }


        public override System.Web.SessionState.SessionStateStoreData CreateNewStoreData(System.Web.HttpContext context, int timeout)
        {

            return new SessionStateStoreData(new SessionStateItemCollection(), SessionStateUtility.GetSessionStaticObjects(context), timeout);
        }


        private SessionStateStoreData GetSessionStoreItem(bool lockRecord, HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {

            SessionStateStoreData item = null;
            lockAge = TimeSpan.Zero;
            lockId = null;
            locked = false;
            actions = 0;

            //string qryStr = string.Empty;
            //// Connection to ODBC database.
            //OracleConnection conn = new OracleConnection(connectionString);
            //// OdbcCommand for database commands.
            //OracleCommand cmd = null;
            //// DataReader to read database record.
            //OracleDataReader reader = null;
            // DateTime to check if current session item is expired.
            DateTime expires = default(DateTime);
            // String to hold serialized SessionStateItemCollection.
            string serializedItems = "";
            // True if a record is found in the database.
            bool foundRecord = false;
            // True if the returned session item is expired and needs to be deleted.
            bool deleteData = false;
            // Timeout value from the data store.
            int timeout = 0;

            try
            {


                if (lockRecord)
                {
                    int roweffected = 0;

                    //Unlock session
                    CustomSession upSession = new CustomSession();
                    upSession.LOCKED = 1;
                    upSession.SESSIONID = id;
                    upSession.ApplicationName = pApplicationName;
                    upSession.OperationMode = "UL";
                    roweffected = CustomSessionBAL.GetInstance().UpdateData(upSession);



                    if (roweffected == 0)
                    {
                        // No record was updated because the record was locked or not found.
                        locked = true;
                    }
                    else
                    {
                        // The record was updated.
                        locked = false;

                    }
                }


                CustomSession currentSession = new CustomSession();
                CustomSession filterSession = new CustomSession();
                filterSession.ApplicationName = pApplicationName;
                filterSession.SESSIONID = id;

                currentSession = CustomSessionBAL.GetInstance().LoadItem(filterSession);

                //if data exist
                if (currentSession.Id > 0)
                {
                    expires = currentSession.EXPIRES;

                    if (DateTime.Compare(expires, DateTime.Now) < 0)
                    {
                        // The record was expired. Mark it as not locked.
                        locked = false;
                        // The session was expired. Mark the data for deletion.
                        deleteData = true;
                    }
                    else
                    {
                        foundRecord = true;
                    }

                    serializedItems = currentSession.SESSIONITEMS;
                    lockId = currentSession.LOCKID;
                    lockAge = DateTime.Now.Subtract(currentSession.LOCKDATE);
                    actions = (SessionStateActions)currentSession.FLAGS;

                    timeout = currentSession.TIMEOUT;
                }
                else
                {
                    locked = false;
                }


                // If the returned session item is expired, 
                // delete the record from the data source.
                if (deleteData)
                {

                    CustomSession delSession = new CustomSession();
                    delSession.ApplicationName = pApplicationName;
                    delSession.SESSIONID = id;

                    CustomSessionBAL.GetInstance().DeleteData(delSession);

                    //HttpContext.Current.Response.Clear();
                    //HttpContext.Current.Response.Write("Session Time Out");
                    //HttpContext.Current.Response.Redirect("~/Shared/Error.aspx");                    
                    deleteData = false;
                }

                if (foundRecord && !(locked == false))
                {
                    lockId = Convert.ToInt32(lockId) + 1;



                    CustomSession upSession = new CustomSession();
                    upSession.ApplicationName = pApplicationName;
                    upSession.SESSIONID = id;
                    upSession.LOCKID = Convert.ToInt32(lockId);
                    upSession.OperationMode = "IL";

                    CustomSessionBAL.GetInstance().UpdateData(upSession);

                    if (actions == SessionStateActions.InitializeItem)
                    {
                        item = CreateNewStoreData(context, pConfig.Timeout.Minutes);
                    }
                    else
                    {
                        item = Deserialize(context, serializedItems, timeout);

                    }

                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetSessionStoreItem");
                    throw new Exception(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {

            }

            return item;
        }

        //
        // SessionStateProviderBase.GetItem
        //

        //public override SessionStateStoreData GetItem(HttpContext context, string id, ref bool locked, ref TimeSpan lockAge, ref object lockId, ref SessionStateActions actionFlags)
        //{

        //    return GetSessionStoreItem(false, context, id, locked, lockAge, lockId, actionFlags);
        //}


        ////
        //// SessionStateProviderBase.GetItemExclusive
        ////

        //public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, ref bool locked, ref TimeSpan lockAge, ref object lockId, ref SessionStateActions actionFlags)
        //{

        //    return GetSessionStoreItem(true, context, id, locked, lockAge, lockId, actionFlags);
        //}

        //
        // Serialize is called by the SetAndReleaseItemExclusive method to 
        // convert the SessionStateItemCollection into a Base64 string to 
        // be stored in an Oracle Clob field.
        //

        public string Serialize(SessionStateItemCollection items)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(ms);

            if ((items != null)) items.Serialize(writer);

            writer.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        //
        // Deserialize is called by the GetSessionStoreItem method to 
        // convert the Base64 string stored in the Oracle Clob field to a 
        // SessionStateItemCollection.
        //

        private SessionStateStoreData Deserialize(HttpContext context, string serializedItems, int timeout)
        {

            MemoryStream ms = new MemoryStream(Convert.FromBase64String(serializedItems));

            SessionStateItemCollection sessionItems = new SessionStateItemCollection();

            if (ms.Length > 0)
            {
                BinaryReader reader = new BinaryReader(ms);
                sessionItems = SessionStateItemCollection.Deserialize(reader);
            }

            return new SessionStateStoreData(sessionItems, SessionStateUtility.GetSessionStaticObjects(context), timeout);
        }

        //
        // SessionStateProviderBase.ReleaseItemExclusive
        //

        public override void ReleaseItemExclusive(System.Web.HttpContext context, string id, object lockId)
        {
            try
            {

                CustomSession upSession = new CustomSession();
                upSession.ApplicationName = pApplicationName;
                upSession.SESSIONID = id;
                upSession.EXPIRES = DateTime.Now.AddMinutes(pConfig.Timeout.Minutes);
                upSession.LOCKID = Convert.ToInt32(lockId);
                upSession.OperationMode = "RIE";
                CustomSessionBAL.GetInstance().UpdateData(upSession);




            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ReleaseItemExclusive");
                    throw new Exception(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {


            }
        }


        //
        // SessionStateProviderBase.RemoveItem
        //

        public override void RemoveItem(System.Web.HttpContext context, string id, object lockId, System.Web.SessionState.SessionStateStoreData item)
        {
            try
            {


                CustomSession delSession = new CustomSession();
                delSession.SESSIONID = id;
                delSession.ApplicationName = pApplicationName;
                delSession.LOCKID = Convert.ToInt32(lockId);
                delSession.OperationMode = "RM";
                CustomSessionBAL.GetInstance().DeleteData(delSession);




            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "RemoveItem");
                    throw new Exception(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {



            }
        }


        //
        // SessionStateProviderBase.CreateUninitializedItem
        //
        public override void CreateUninitializedItem(System.Web.HttpContext context, string id, int timeout)
        {
            try
            {
               
                CustomSession newSession = new CustomSession();
                newSession.SESSIONID = id;
                newSession.ApplicationName = pApplicationName;
                newSession.CREATED = DateTime.Now;
                newSession.EXPIRES = DateTime.Now.AddMinutes((double)timeout);
                newSession.LOCKDATE = DateTime.Now;
                newSession.LOCKID = 0;
                newSession.LOCKED = 0;
                newSession.SESSIONITEMS = "";
                newSession.FLAGS = 1;
                newSession.TIMEOUT = timeout;

                CustomSessionBAL.GetInstance().InsertData(newSession);
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "CreateUninitializedItem");
                    throw e;
                }
                else
                    throw e;
            }
            finally
            {
                            
            }

        }


        //
        // SessionStateProviderBase.ResetItemTimeout
        //

        public override void ResetItemTimeout(System.Web.HttpContext context, string id)
        {
            try
            {

                CustomSession upSession = new CustomSession();
                upSession.EXPIRES = DateTime.Now.AddMinutes(pConfig.Timeout.Minutes);
                upSession.ApplicationName = pApplicationName;
                upSession.SESSIONID = id;

                CustomSessionBAL.GetInstance().UpdateData(upSession);

            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ResetItemTimeout");
                    throw new Exception(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {


            }
        }

        //
        // SessionStateProviderBase.InitializeRequest
        //

        public override void InitializeRequest(HttpContext context)
        {

        }


        //
        // SessionStateProviderBase.EndRequest
        //

        public override void EndRequest(HttpContext context)
        {

        }


        //
        // WriteToEventLog
        // This is a helper function that writes exception detail to the 
        // event log. Exceptions are written to the event log as a security
        // measure to ensure Private database details are not returned to 
        // browser. If a method does not Return a status or Boolean
        // indicating the action succeeded or failed, the caller also 
        // throws a generic exception.
        //

        private void WriteToEventLog(Exception e, string action)
        {
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message = "An exception occurred communicating with the data source." + "\n\n";
            message += "Action: " + action + "\n" + "\n";
            message += "Exception: " + e.ToString();

            log.WriteEntry(message);
        }


        public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            return GetSessionStoreItem(false, context, id, out locked, out lockAge, out lockId, out actions);
        }

        public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            return GetSessionStoreItem(true, context, id, out locked, out lockAge, out lockId, out actions);
        }
    }
}