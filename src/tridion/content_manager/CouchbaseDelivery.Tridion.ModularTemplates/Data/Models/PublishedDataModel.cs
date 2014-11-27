using System;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models
{
    /// <summary>
    /// Represents the published out model for a page
    /// </summary>
    public abstract class PublishedDataModel
    {
        public DateTime PublishDate { get; set; }

        public abstract string Type { get; }
    }
}
