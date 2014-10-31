using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content
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
