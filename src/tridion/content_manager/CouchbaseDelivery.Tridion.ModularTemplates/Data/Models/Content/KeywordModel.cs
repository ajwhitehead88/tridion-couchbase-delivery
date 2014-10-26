using System.Runtime.Serialization;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content
{
    [DataContract]
    [KnownType(typeof(KeywordModel))]
    public class KeywordModel  : IdentifiableObjectModel
    {
        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public IEnumerable<BaseFieldModel> Metadata { get; set; }
    }
}
