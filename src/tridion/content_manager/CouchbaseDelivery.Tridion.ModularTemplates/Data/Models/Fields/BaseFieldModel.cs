using System.Runtime.Serialization;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    [DataContract]
    [KnownType(typeof(BaseFieldModel))]
    public class BaseFieldModel
    {
        [DataMember]
        public string Key { get; set; }
    }
}
