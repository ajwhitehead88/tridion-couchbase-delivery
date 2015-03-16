using CouchbaseDelivery.Data.ContentModel.Contract.Model.Structure;

namespace CouchbaseDelivery.Data.ContentModel.Contract.Model
{
    /// <summary>
    /// Represents the published out model for a page
    /// </summary>
    public interface IPagePublishedDataModel : IPublishedDataModel
    {
        IPageModel Page { get; }

        IStructureGroupModel Parent { get; }

        IPublicationModel Publication { get; }
    }
}
