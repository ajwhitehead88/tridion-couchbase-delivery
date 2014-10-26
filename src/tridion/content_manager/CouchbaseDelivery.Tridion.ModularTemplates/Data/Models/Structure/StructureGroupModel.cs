using System.Runtime.Serialization;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure
{
    [DataContract]
    [KnownType(typeof(StructureGroupModel))]
    public class StructureGroupModel : IdentifiableObjectModel
    {
        [DataMember(IsRequired = true)]
        public string PublishedUrl { get; set; }

        [DataMember]
        public IEnumerable<BaseFieldModel> Metadata { get; set; }
    }
}
