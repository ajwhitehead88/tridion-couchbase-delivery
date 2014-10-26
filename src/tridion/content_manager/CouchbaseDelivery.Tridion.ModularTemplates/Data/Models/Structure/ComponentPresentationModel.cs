using System.Runtime.Serialization;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Layout;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure
{
    [DataContract]
    [KnownType(typeof(ComponentPresentationModel))]
    public class ComponentPresentationModel
    {
        [DataMember(IsRequired = true)]
        public ComponentModel ComponentModel { get; set; }

        [DataMember(IsRequired = true)]
        public ComponentTemplateModel ComponentTemplateModel { get; set; }
    }
}
