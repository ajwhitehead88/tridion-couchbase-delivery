using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Layout
{
    /// <summary>
    /// Models a template
    /// </summary>
    public class TemplateModel : IdentifiableObjectModel
    {
        public FieldSetModel Metadata { get; set; }
    }
}
