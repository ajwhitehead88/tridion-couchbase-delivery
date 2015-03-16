using CouchbaseDelivery.Data.ContentModel.Contract.Model.Structure;

namespace CouchbaseDelivery.Data.ContentModel.Contract.Model
{
    /// <summary>
    /// Represents the published out model for a dynamic presentation
    /// </summary>
    public interface IPresentationPublishedDataModel : IPublishedDataModel
    {
        IComponentPresentationModel Presentation { get; }
    }
}
