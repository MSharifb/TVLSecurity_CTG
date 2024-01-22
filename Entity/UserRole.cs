using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;

namespace Entity
{
    [DataContract]
    public class UserRole : EntityBase
    {
        [DataMember, DataColumn(true)]
        public int UserId { set; get; }

        [DataMember, DataColumn(true)]
        public int RoleId { set; get; }

        [DataMember, DataColumn(true)]
        public int UserRoleId { set; get; }

        [DataMember, DataColumn(true)]
        public bool IsAssignedRole { set; get; }

        [DataMember, DataColumn(false)]
        public int ApplicationId { set; get; }

        [DataMember, DataColumn(false)]
        public int ModuleId { set; get; }
    }
}
