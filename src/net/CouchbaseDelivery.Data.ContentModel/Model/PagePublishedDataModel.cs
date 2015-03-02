using CouchbaseDelivery.Data.ContentModel.Model.Structure;

namespace CouchbaseDelivery.Data.ContentModel.Model
{
    /// <summary>
    /// Represents the published out model for a page
    /// </summary>
    public class PagePublishedDataModel : PublishedDataModel
    {
        public PageModel Page { get; set; }

        public StructureGroupModel Parent { get; set; }

        public PublicationModel Publication { get; set; }
    }
}
