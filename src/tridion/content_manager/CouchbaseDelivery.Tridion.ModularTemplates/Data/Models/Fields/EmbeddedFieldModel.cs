using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    [DataContract]
    [KnownType(typeof(EmbeddedFieldModel))]
    public class EmbeddedFieldModel : BaseFieldModel
    {
        [DataMember(IsRequired = true)]
        public IEnumerable<IEnumerable<BaseFieldModel>> Values { get; set; }
    }
}
