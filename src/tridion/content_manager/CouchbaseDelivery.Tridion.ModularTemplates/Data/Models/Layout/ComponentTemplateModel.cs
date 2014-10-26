using System.Runtime.Serialization;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Layout
{
    [DataContract]
    [KnownType(typeof(ComponentTemplateModel))]
    public class ComponentTemplateModel : IdentifiableObjectModel
    {
        [DataMember]
        public IEnumerable<BaseFieldModel> Metadata { get; set; }
    }
}
