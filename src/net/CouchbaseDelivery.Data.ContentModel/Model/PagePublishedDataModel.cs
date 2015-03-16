using CouchbaseDelivery.Data.ContentModel.Contract.Model;
using CouchbaseDelivery.Data.ContentModel.Contract.Model.Structure;
using CouchbaseDelivery.Data.ContentModel.Model.Structure;
using Newtonsoft.Json;

namespace CouchbaseDelivery.Data.ContentModel.Model
{
    /// <summary>
    /// Represents the published out model for a page
    /// </summary>
    public class PagePublishedDataModel : PublishedDataModel, IPagePublishedDataModel
    {
        public PageModel Page { get; set; }

        public StructureGroupModel Parent { get; set; }

        public PublicationModel Publication { get; set; }

        [JsonIgnore]
        IPageModel IPagePublishedDataModel.Page { get { return Page; } }

        [JsonIgnore]
        IStructureGroupModel IPagePublishedDataModel.Parent { get { return Parent; } }

        [JsonIgnore]
        IPublicationModel IPagePublishedDataModel.Publication { get { return Publication; } }
    }
}
