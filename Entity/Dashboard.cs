using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;


namespace Entity
{

    [DataContract]
    public class Dashboard : EntityBase
    {        
        [DataMember, DataColumn(true)]
        public string ZoneName { set; get; }

        [DataMember, DataColumn(true)]
        public string TotalUser { set; get; }

        [DataMember, DataColumn(true)]
        public string ZoneUser { set; get; }

    }
}
