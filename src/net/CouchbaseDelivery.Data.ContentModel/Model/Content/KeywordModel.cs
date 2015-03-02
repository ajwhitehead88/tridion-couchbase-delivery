using CouchbaseDelivery.Data.ContentModel.Model.Structure;

namespace CouchbaseDelivery.Data.ContentModel.Model.Content
{
    /// <summary>
    /// Models a keyword and it's metadata
    /// </summary>
    public class KeywordModel : IdentifiableObjectModel
    {
        public string Key { get; set; }

        public FieldSetModel Metadata { get; set; }
    }
}
