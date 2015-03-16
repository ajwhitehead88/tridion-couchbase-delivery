
namespace CouchbaseDelivery.Data.ContentModel.Contract.Model.Content
{
    /// <summary>
    /// Represents the published out model for a page
    /// </summary>
    public interface IBinaryModel : IPublishedDataModel, IIdentifiableObjectModel
    {
        string PublishedUrl { get; }

        string Content { get; }
    }
}
