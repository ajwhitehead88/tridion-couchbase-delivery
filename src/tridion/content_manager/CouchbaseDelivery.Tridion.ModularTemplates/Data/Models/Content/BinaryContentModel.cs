using System.Runtime.Serialization;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content
{
    [DataContract]
    [KnownType(typeof(BinaryContentModel))]
    public class BinaryContentModel
    {
        [DataMember(IsRequired = true)]
        public string Filename { get; set; }

        [DataMember(IsRequired = true)]
        public string BinaryContent { get; set; }
    }
}
