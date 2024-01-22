using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;
using System.ComponentModel.DataAnnotations;




namespace Entity
{
    [DataContract]
   public class User:EntityBase 
    {
        [DataMember, DataColumn(true)] 
        public int UserId { set; get; }

        [DataMember, DataColumn(true)] 
        public string FirstName { set; get; }

        [DataMember, DataColumn(true)] 
        public string LastName { set; get; }

        [DataMember, DataColumn(true)] 
        public string LoginId { set; get; }

        [RegularExpression(@"^([\w-_]+\.)*[\w-_]+@([\w-_]+\.)*[\w-_]+\.[\w-_]+$",ErrorMessage="Not a valid email")] 
        [DataMember, DataColumn(true)] 
        public string EmailAddress { set; get; }

        [DataMember, DataColumn(true)] 
        public string Phone { set; get; }

        [DataMember, DataColumn(true)] 
        public string Password { set; get; }

        [DataMember, DataColumn(true)] 
        public bool NeverExperied { set; get; }

        [DataMember, DataColumn(true)] 
        public string CreatedBy { set; get; }

        [DataMember, DataColumn(true)] 
        public DateTime CreatedDate { set; get; }

        [DataMember, DataColumn(true)] 
        public string UpdatedBy { set; get; }

        [DataMember, DataColumn(true)] 
        public DateTime UpdatedDate { set; get; }

        [DataMember, DataColumn(true)] 
        public DateTime LastLoginDate { set; get; }

        [DataMember, DataColumn(true)] 
        public Int32? GroupId { set; get; }

        [DataMember, DataColumn(true)]
        public Int32 ZoneId { set; get; }

        [DataMember, DataColumn(true)]
        public string ZoneName { set; get; }

       [DataMember, DataColumn(true)] 
       public bool IsLockedOut{set;get;}

       [DataMember, DataColumn(true)] 
       public DateTime LastPasswordChangedDate{set;get;}

       [DataMember, DataColumn(true)] 
       public DateTime LastLockoutDate{set;get;}
     
       [DataMember, DataColumn(true)] 
       public int FailedPasswordAttemptCount{set;get;}

       [DataMember, DataColumn(true)] 
       public string Comment{set;get;}

       [DataMember, DataColumn(true)] 
       public int ApplicationId { set; get; }

       [DataMember, DataColumn(true)]
       public string EmpId { set; get; }

       [DataMember, DataColumn(true)]
       public bool ChangePasswordAtFirstLogin { set; get; }

       [DataMember, DataColumn(true)]
       public bool Status { set; get; }

    }
}
