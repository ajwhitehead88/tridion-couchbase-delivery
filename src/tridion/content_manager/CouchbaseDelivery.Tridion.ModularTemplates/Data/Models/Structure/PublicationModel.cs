using System.Runtime.Serialization;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure
{
    [DataContract]
    [KnownType(typeof(PublicationModel))]
    public class PublicationModel : IdentifiableObjectModel
    {
        [DataMember(IsRequired = true)]
        public string PublicationUrl { get; set; }

        [DataMember(IsRequired = true)]
        public string MultimediaUrl { get; set; }

        [DataMember]
        public IEnumerable<BaseFieldModel> Metadata { get; set; }
    }
}
