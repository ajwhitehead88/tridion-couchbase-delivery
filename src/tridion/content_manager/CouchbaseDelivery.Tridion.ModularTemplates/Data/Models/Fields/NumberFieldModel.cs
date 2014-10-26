using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    [DataContract]
    [KnownType(typeof(NumberFieldModel))]
    public class NumberFieldModel : BaseFieldModel
    {
        [DataMember(IsRequired = true)]
        public IEnumerable<double> Values { get; set; }
    }
}
