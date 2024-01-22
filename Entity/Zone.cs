using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;

namespace Entity
{
    [DataContract]
    public class Zone : EntityBase
    {
        [DataMember, DataColumn(true)]
        public Int32 ZoneId { set; get; }

        [DataMember, DataColumn(true)]
        public string ZoneName { set; get; }

        [DataMember, DataColumn(true)]
        public bool IsAssignedZone { set; get; }

        [DataMember, DataColumn(true)]
        public string EmpId { set; get; }

        [DataMember, DataColumn(true)]
        public string CreatedBy { set; get; }

        [DataMember, DataColumn(true)]
        public DateTime CreatedDate { set; get; }

        [DataMember, DataColumn(true)]
        public string UpdatedBy { set; get; }

        [DataMember, DataColumn(true)]
        public DateTime UpdatedDate { set; get; }

    }
}
