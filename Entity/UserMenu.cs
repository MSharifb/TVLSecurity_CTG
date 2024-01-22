using PrimaryBaseLibrary;
using System.Runtime.Serialization;

namespace Entity
{
    [DataContract]
    public class UserMenu : EntityBase
    {
        [DataMember, DataColumn(true)]
        public int UserId { set; get; }

        [DataMember, DataColumn(false)]
        public int ApplicationId { set; get; }

        [DataMember, DataColumn(false)]
        public int ModuleId { set; get; }

    }
}
