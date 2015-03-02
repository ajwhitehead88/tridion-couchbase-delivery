using System.Collections.Generic;
using CouchbaseDelivery.Data.ContentModel.Model.Layout;

namespace CouchbaseDelivery.Data.ContentModel.Model.Structure
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
