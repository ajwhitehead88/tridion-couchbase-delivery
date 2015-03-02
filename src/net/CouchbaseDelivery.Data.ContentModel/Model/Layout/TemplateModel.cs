using CouchbaseDelivery.Data.ContentModel.Model.Structure;

namespace CouchbaseDelivery.Data.ContentModel.Model.Layout
{
    /// <summary>
    /// Models a template
    /// </summary>
    public class TemplateModel : IdentifiableObjectModel
    {
        public int Priority { get; set; }

        public FieldSetModel Metadata { get; set; }
    }
}
