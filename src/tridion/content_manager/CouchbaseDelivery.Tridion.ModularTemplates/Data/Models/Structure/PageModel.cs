using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Layout;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure
{
    /// <summary>
    /// Models a published page
    /// </summary>
    public class PageModel : IdentifiableObjectModel
    {
        public string PublishedUrl { get; set; }

        public TemplateModel TemplateModel { get; set; }

        public IEnumerable<ComponentPresentationModel> ComponentPresentations { get; set; }

        public FieldSetModel Metadata { get; set; }
    }
}
