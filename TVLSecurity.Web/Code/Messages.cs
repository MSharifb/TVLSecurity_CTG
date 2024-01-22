using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TVLSecurity.Web.Code
{
    public class Messages
    {

        //Now I am writing static messages here but in future this message will come
        //from XML

        public enum MessageIdEnum
        {
           SavedSuccessFully =1,
           UpdatedSuccessFully=2,
           DeletedSuccessFully=3,
           NoDataFound=4,
           CouldntSave=5, 
           NotAbleToLoad=6,
           DuplicateData = -2627
                        
        }


        public enum ErroMessageCodeEnum
        {
            DuplicateData = 2601,
           // ForeignKeyConstraint = 2501
        }


        public static string GetErrorMessage(ErroMessageCodeEnum messageCode)
        {
            string msg = string.Empty;

            switch (messageCode)
            {
                case ErroMessageCodeEnum.DuplicateData:
                    msg = "This information already exists.";
                    break;
                //case ErroMessageCodeEnum.ForeignKeyConstraint:
                //    msg = "Not able to delete.There is a dependency";
                //    break;               
            }

            return msg;
        }

        public static string GetMessage(MessageIdEnum msgId)
        {
            string msg = string.Empty;

            switch (msgId)
            {
                case MessageIdEnum.SavedSuccessFully:
                    msg = "Information saved successfully.";
                    break;
                case MessageIdEnum.UpdatedSuccessFully: 
                    msg = "Information updated successfully.";
                    break;
                case MessageIdEnum.DeletedSuccessFully:
                    msg = "Information deleted successfully.";
                    break;
                case MessageIdEnum.NoDataFound:
                    msg = "No data found.";
                    break;
                case MessageIdEnum.CouldntSave:
                    msg = "Not able to save data.";
                    break;
                case MessageIdEnum.NotAbleToLoad:
                    msg = "Not able to Load";
                    break;
                case MessageIdEnum.DuplicateData:
                    msg = "Information alredy exist.";
                    break;
                default:
                    msg = "Not able to save data";
                    break;
            }

            return msg;
        }
        
    }
}
