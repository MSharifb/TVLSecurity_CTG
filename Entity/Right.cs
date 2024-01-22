using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;

namespace Entity
{
    [DataContract]
    public class Right : EntityBase
    {
        [DataMember, DataColumn(true)]
        public Int32 RightId { set; get; }

        [DataMember, DataColumn(false)]
        public Int32 RoleId { set; get; }

        [DataMember, DataColumn(true)]
        public string  RightTitle { set; get; }

        [DataMember, DataColumn(true)]
        public string Description { set; get; }


        [DataMember, DataColumn(true)]
        public string RightName { set; get; }


        [DataMember, DataColumn(true)]
        public bool IsAssignedRight { set; get; }


        [DataMember, DataColumn(false)]
        public string LoginId { set; get; }

        [DataMember, DataColumn(true)]
        public int ApplicationId { set; get; }

        [DataMember, DataColumn(true)]
        public int ModuleId { set; get; }

         [DataMember, DataColumn(true)]
        public string ApplicationName { set; get; }

         [DataMember, DataColumn(true)]
        public string ModuleName { set; get; }


    }
}
