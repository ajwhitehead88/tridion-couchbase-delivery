using System;

namespace CouchbaseDelivery.Data.ContentModel.Contract.Model
{
    /// <summary>
    /// Represents the published out model for a page
    /// </summary>
    public interface IPublishedDataModel
    {
        DateTime PublishDate { get; }

        int PublicationId { get; }
    }
}
