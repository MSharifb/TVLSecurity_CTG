using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;

namespace Entity
{
    [DataContract]
    public class Application : EntityBase
    {
        [DataMember, DataColumn(true)]
        public int ApplicationId { set; get; }
        [DataMember, DataColumn(true)]
        public string ApplicationName { set; get; }
        [DataMember, DataColumn(true)]
        public string ApplicationTitle { set; get; }
        [DataMember, DataColumn(true)]
        public string Description { set; get; }
        [DataMember, DataColumn(true)]
        public bool IsVerificationEnable { get; set; }
    }
}
