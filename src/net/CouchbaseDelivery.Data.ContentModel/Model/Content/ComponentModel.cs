using CouchbaseDelivery.Data.ContentModel.Model.Structure;

namespace CouchbaseDelivery.Data.ContentModel.Model.Content
{
    public class ComponentModel : IdentifiableObjectModel
    {
        public FieldSetModel Content { get; set; }

        public FieldSetModel Metadata { get; set; }

        public string BinaryUrl { get; set; }
    }
}
