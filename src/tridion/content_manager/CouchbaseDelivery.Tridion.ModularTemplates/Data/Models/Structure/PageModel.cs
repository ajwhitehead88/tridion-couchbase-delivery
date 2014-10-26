using System.Runtime.Serialization;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Layout;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure
{
    [DataContract]
    [KnownType(typeof(PageModel))]
    public class PageModel : IdentifiableObjectModel
    {
        [DataMember(IsRequired = true)]
        public string PublishedUrl { get; set; }

        [DataMember(IsRequired = true)]
        public PageTemplateModel PageTemplateModel { get; set; }

        [DataMember]
        public IEnumerable<ComponentPresentationModel> ComponentPresentations { get; set; }

        [DataMember]
        public IEnumerable<BaseFieldModel> Metadata { get; set; }
    }
}
