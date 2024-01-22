using System.Runtime.Serialization;

namespace PrimaryBaseLibrary
{
    [DataContract]
    public class EntityBase
    {
         
        public string OperationMode { set; get; }
        public bool IsValid{set;get;}        
       
       
    }
}
