using CouchbaseDelivery.Data.ContentModel.Contract.Model;

namespace CouchbaseDelivery.Data.ContentModel.Model
{
    /// <summary>
    /// Base class for common fields for Tridion identifiable objects
    /// </summary>
    public abstract class IdentifiableObjectModel : IIdentifiableObjectModel
    {
        public string TcmUri { get; set; }

        public string Title { get; set; }

        public string SchemaName { get; set; }
    }
}
