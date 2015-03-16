using CouchbaseDelivery.Data.ContentModel.Contract.Model;
using CouchbaseDelivery.Data.ContentModel.Contract.Model.Structure;
using CouchbaseDelivery.Data.ContentModel.Model.Structure;
using Newtonsoft.Json;

namespace CouchbaseDelivery.Data.ContentModel.Model
{
    /// <summary>
    /// Represents the published out model for a dynamic presentation
    /// </summary>
    public class PresentationPublishedDataModel : PublishedDataModel, IPresentationPublishedDataModel
    {
        public ComponentPresentationModel Presentation { get; set; }
        
        [JsonIgnore]
        IComponentPresentationModel IPresentationPublishedDataModel.Presentation { get { return Presentation; } }
    }
}
