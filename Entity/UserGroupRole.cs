using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;

namespace Entity
{
    [DataContract]
    public class UserGroupRole : EntityBase
    {
        [DataMember, DataColumn(true)]
        public Int32 GroupRoleId { set; get; }
        [DataMember, DataColumn(true)]
        public Int32 GroupId { set; get; }
        [DataMember, DataColumn(true)]
        public Int32 RoleId { set; get; }
        [DataMember, DataColumn(false)]
        public Int32 ApplicationId { set; get; }
        [DataMember, DataColumn(false)]
        public Int32 ModuleId { set; get; }
    }
}
