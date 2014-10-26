using System.Runtime.Serialization;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models
{
    [DataContract]
    [KnownType(typeof(IdentifiableObjectModel))]
    public abstract class IdentifiableObjectModel
    {
        [DataMember(IsRequired = true)]
        public string TcmUri { get; set; }

        [DataMember(IsRequired = true)]
        public string Title { get; set; }

        [DataMember]
        public string SchemaName { get; set; }
    }
}
