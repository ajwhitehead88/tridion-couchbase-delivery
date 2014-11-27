using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models
{
    /// <summary>
    /// Represents the published out model for a dynamic presentation
    /// </summary>
    public class PresentationPublishedDataModel : PublishedDataModel
    {
        private const string TypeValue = "presentation";

        public ComponentPresentationModel Presentation { get; set; }

        public override string Type { get { return TypeValue; } }
    }
}
