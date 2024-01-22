using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;

namespace Entity
{
    public class Module : EntityBase
    {
        [DataMember, DataColumn(true)]
        public int ModuleId { set; get; }

        [DataMember, DataColumn(true)]
        public string ModuleName { set; get; }

        [DataMember, DataColumn(true)]
        public string ModuleTitle { set; get; }

        [DataMember, DataColumn(true)]
        public string Description { set; get; }

        [DataMember, DataColumn(true)]
        public int ApplicationId { set; get; }

        [DataMember, DataColumn(true)]
        public string ApplicationName { set; get; }

        [DataMember, DataColumn(true)]
        public int SortOrder { set; get; }
    }
}
