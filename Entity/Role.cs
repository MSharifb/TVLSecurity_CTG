using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;


namespace Entity
{
    [DataContract]
    public class Role : EntityBase
    {
        [DataMember, DataColumn(true)] 
        public Int32 RoleId { set; get; }

        [DataMember, DataColumn(true)] 
        public string RoleName { set; get; }

        [DataMember, DataColumn(true)] 
        public string Description { set; get; }

        [DataMember, DataColumn(true)]
        public int ModuleId { set; get; }

        [DataMember, DataColumn(true)]
        public string ModuleTitle { set; get; }

        [DataMember, DataColumn(true)]
        public bool IsAssignedRole { set; get; }

        [DataMember, DataColumn(false)]
        public Int32 UserId { set; get; }

        [DataMember, DataColumn(true)]
        public Int32 ApplicationId { set; get; }

        [DataMember, DataColumn(true)]
        public string ApplicationTitle { set; get; }

        [DataMember, DataColumn(false)]
        public Int32 UserGroupId { set; get; }

    }
}

