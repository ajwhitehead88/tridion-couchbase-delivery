
namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure
{
    public class PublicationModel : IdentifiableObjectModel
    {
        public string PublicationUrl { get; set; }

        public string MultimediaUrl { get; set; }

        public FieldSetModel Metadata { get; set; }
    }
}
