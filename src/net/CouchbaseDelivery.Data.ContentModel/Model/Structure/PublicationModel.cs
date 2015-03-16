using CouchbaseDelivery.Data.ContentModel.Contract.Model.Content;
using CouchbaseDelivery.Data.ContentModel.Contract.Model.Structure;
using CouchbaseDelivery.Data.ContentModel.Model.Content;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Model.Structure
{
    /// <summary>
    /// Models a publication
    /// </summary>
    public class PublicationModel : IdentifiableObjectModel, IPublicationModel
    {
        public string PublicationUrl { get; set; }

        public string MultimediaUrl { get; set; }

        public IEnumerable<FieldModel> Metadata { get; set; }

        [JsonIgnore]
        IEnumerable<IFieldModel> IPublicationModel.Metadata { get { return Metadata; } }
    }
}
