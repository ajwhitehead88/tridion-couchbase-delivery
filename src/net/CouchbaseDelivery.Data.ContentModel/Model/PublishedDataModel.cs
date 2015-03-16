using CouchbaseDelivery.Data.ContentModel.Contract.Model;
using System;

namespace CouchbaseDelivery.Data.ContentModel.Model
{
    /// <summary>
    /// Represents the published out model for a page
    /// </summary>
    public abstract class PublishedDataModel : IPublishedDataModel
    {
        public DateTime PublishDate { get; set; }

        public int PublicationId { get; set; }
    }
}
