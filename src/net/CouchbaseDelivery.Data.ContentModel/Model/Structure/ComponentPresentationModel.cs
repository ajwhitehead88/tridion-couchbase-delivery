using CouchbaseDelivery.Data.ContentModel.Model.Content;
using CouchbaseDelivery.Data.ContentModel.Model.Layout;

namespace CouchbaseDelivery.Data.ContentModel.Model.Structure
{
    /// <summary>
    /// Models a single component presentation
    /// </summary>
    public class ComponentPresentationModel
    {
        public ComponentModel ComponentModel { get; set; }

        public TemplateModel TemplateModel { get; set; }
    }
}
