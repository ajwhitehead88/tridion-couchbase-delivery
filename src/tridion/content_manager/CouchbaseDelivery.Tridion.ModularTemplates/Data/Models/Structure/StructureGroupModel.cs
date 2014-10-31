namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure
{
    /// <summary>
    /// Models a structure group
    /// </summary>
    public class StructureGroupModel : IdentifiableObjectModel
    {
        public string PublishedUrl { get; set; }

        public FieldSetModel Metadata { get; set; }
    }
}
