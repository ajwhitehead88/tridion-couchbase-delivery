namespace CouchbaseDelivery.Data.ContentModel.Contract.Model
{
    /// <summary>
    /// Base class for common fields for Tridion identifiable objects
    /// </summary>
    public interface IIdentifiableObjectModel
    {
        string TcmUri { get; }

        string Title { get; }

        string SchemaName { get; }
    }
}
