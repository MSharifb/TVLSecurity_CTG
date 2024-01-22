using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;


namespace Entity
{
    [DataContract]
   public  class CustomSession:EntityBase
    {
        [DataMember, DataColumn(true)]
        public string SESSIONID { set; get; }

        [DataMember, DataColumn(true)]
        public Int32 ApplicationId { set; get; }

        [DataMember, DataColumn(true)]
        public DateTime CREATED { set; get; }

        [DataMember, DataColumn(true)]
        public DateTime EXPIRES { set; get; }

        [DataMember, DataColumn(true)]
        public DateTime LOCKDATE { set; get; }

        [DataMember, DataColumn(true)]
        public int LOCKID { set; get; }

        [DataMember, DataColumn(true)]
        public int TIMEOUT { set; get; }

        [DataMember, DataColumn(true)]
        public int LOCKED { set; get; }

        [DataMember, DataColumn(true)]
        public string SESSIONITEMS { set; get; }

        [DataMember, DataColumn(true)]
        public int FLAGS { set; get; }

        [DataMember, DataColumn(true)]
        public int Id { set; get; }

        [DataMember, DataColumn(true)]
        public string ApplicationName { set; get; }


    }
}
