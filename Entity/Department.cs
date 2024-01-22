using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;

namespace Entity
{
    [DataContract]
    public class Department : EntityBase
    {              
        [DataMember, DataColumn(true)]
        public string strDepartmentID { get; set; }

        [DataMember, DataColumn(true)]
        public string strDepartment { set; get; }
    }
}
