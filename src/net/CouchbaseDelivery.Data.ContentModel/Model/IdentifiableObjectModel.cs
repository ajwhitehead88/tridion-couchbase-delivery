namespace CouchbaseDelivery.Data.ContentModel.Model
{
    /// <summary>
    /// Base class for common fields for Tridion identifiable objects
    /// </summary>
    public abstract class IdentifiableObjectModel
    {
        public string TcmUri { get; set; }

        public string Title { get; set; }

        public string SchemaName { get; set; }
    }
}
