using System.Runtime.Serialization;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content
{
    [DataContract]
    [KnownType(typeof(ComponentModel))]
    public class ComponentModel : IdentifiableObjectModel
    {
        [DataMember(IsRequired = true)]
        public IEnumerable<BaseFieldModel> Content { get; set; }

        [DataMember]
        public IEnumerable<BaseFieldModel> Metadata { get; set; }

        [DataMember]
        public BinaryContentModel BinaryContent { get; set; } 
    }
}
