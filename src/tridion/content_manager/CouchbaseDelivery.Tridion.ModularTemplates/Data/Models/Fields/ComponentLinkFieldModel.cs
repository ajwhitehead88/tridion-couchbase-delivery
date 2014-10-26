using System.Runtime.Serialization;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    [DataContract]
    [KnownType(typeof(ComponentLinkFieldModel))]
    public class ComponentLinkFieldModel : BaseFieldModel
    {
        [DataMember(IsRequired = true)]
        public IEnumerable<ComponentModel> Values { get; set; }
    }
}
