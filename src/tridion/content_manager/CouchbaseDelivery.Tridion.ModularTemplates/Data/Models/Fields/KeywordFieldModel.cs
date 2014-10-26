using System.Runtime.Serialization;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    [DataContract]
    [KnownType(typeof(KeywordFieldModel))]
    public class KeywordFieldModel : BaseFieldModel
    {
        [DataMember(IsRequired = true)]
        public IEnumerable<KeywordModel> Values { get; set; }
    }
}
