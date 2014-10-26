using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    [DataContract]
    [KnownType(typeof(StringFieldModel))]
    public class StringFieldModel : BaseFieldModel
    {
        [DataMember(IsRequired = true)]
        public IEnumerable<string> Values { get; set; }
    }
}
