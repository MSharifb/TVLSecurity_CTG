using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;

namespace Entity
{
    [DataContract]
    public class UserRoleInfo : EntityBase
    {
        [DataMember, DataColumn(true)]
        public int UserId { set; get; }

        [DataMember, DataColumn(true)]
        public int RoleId { set; get; }

        [DataMember, DataColumn(true)]
        public int UserRoleId { set; get; }

    }
}
