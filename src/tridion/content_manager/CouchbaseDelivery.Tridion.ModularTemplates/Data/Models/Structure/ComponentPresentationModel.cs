using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Layout;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure
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
