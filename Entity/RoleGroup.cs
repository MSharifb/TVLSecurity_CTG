using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;

namespace Entity
{
    [DataContract]
   public class RoleGroup : EntityBase 
    {
        [DataMember, DataColumn(true)]
        public Int32 RoleGroupsId { set; get; }

        [DataMember, DataColumn(true)]
        public string RoleGroupsTitle { set; get; }

        [DataMember, DataColumn(true)]
        public Int32 ApplicationId { set; get; }
    }
}
