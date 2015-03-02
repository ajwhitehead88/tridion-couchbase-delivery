using CouchbaseDelivery.Data.ContentModel.Model.Structure;

namespace CouchbaseDelivery.Data.ContentModel.Model
{
    /// <summary>
    /// Represents the published out model for a dynamic presentation
    /// </summary>
    public class PresentationPublishedDataModel : PublishedDataModel
    {
        public ComponentPresentationModel Presentation { get; set; }
    }
}
