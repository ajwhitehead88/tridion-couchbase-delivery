using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure;
using System;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models
{
    /// <summary>
    /// Represents the published out model for a page
    /// </summary>
    public class PublishedDataModel
    {
        public PageModel Page { get; set; }

        public StructureGroupModel Parent { get; set; }

        public PublicationModel Publication { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
