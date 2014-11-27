using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models
{
    /// <summary>
    /// Represents the published out model for a dynamic presentation
    /// </summary>
    public class PresentationPublishedDataModel : PublishedDataModel
    {
        public ComponentPresentationModel Presentation { get; set; }
    }
}
