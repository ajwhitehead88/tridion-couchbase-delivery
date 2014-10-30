using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Layout;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure
{
    public class PageModel : IdentifiableObjectModel
    {
        public string PublishedUrl { get; set; }

        public PageTemplateModel PageTemplateModel { get; set; }

        public IEnumerable<ComponentPresentationModel> ComponentPresentations { get; set; }

        public FieldSetModel Metadata { get; set; }
    }
}
