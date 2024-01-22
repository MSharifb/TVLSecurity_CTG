using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;

namespace Entity
{
    [DataContract]
    public class UserGroup : EntityBase
    {
        [DataMember, DataColumn(true)]
        public Int32 GroupId { set; get; }

        [DataMember, DataColumn(true)]
        public string GroupName { set; get; }

        [DataMember, DataColumn(true)]
        public string Description { set; get; }

        [DataMember, DataColumn(false)]
        public int ApplicationId { set; get; }

        [DataMember, DataColumn(false)]
        public int ModuleId { set; get; }

    }
}
