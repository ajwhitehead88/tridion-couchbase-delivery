using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content
{
    public class ComponentModel : IdentifiableObjectModel
    {
        public FieldSetModel Content { get; set; }

        public FieldSetModel Metadata { get; set; }

        public BinaryContentModel BinaryContent { get; set; }
    }
}
