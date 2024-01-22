using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;

namespace Entity
{
    [DataContract]
    public class Employee : EntityBase
    {
        [DataMember, DataColumn(true)]
        public string EmpId { set; get; }

        [DataMember, DataColumn(true)]
        public string EmpName { set; get; }

        [DataMember, DataColumn(true)]
        public string strDesignationID { get; set; }

        [DataMember, DataColumn(true)]
        public string strDepartmentID { get; set; }

        [DataMember, DataColumn(true)]
        public string strEmail { get; set; }

        [DataMember, DataColumn(true)]
        public string Designation { set; get; }

        [DataMember, DataColumn(true)]
        public string Department { set; get; }

        [DataMember, DataColumn(false)]
        public int ApplicationId { set; get; }

        [DataMember, DataColumn(false)]
        public int ZoneId { set; get; }

        [DataMember, DataColumn(true)]
        public string MobileNo { set; get; }

    }
}
