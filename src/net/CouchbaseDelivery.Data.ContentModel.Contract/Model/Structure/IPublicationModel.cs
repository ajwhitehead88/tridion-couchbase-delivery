using CouchbaseDelivery.Data.ContentModel.Contract.Model.Content;
using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Contract.Model.Structure
{
    /// <summary>
    /// Models a publication
    /// </summary>
    public interface IPublicationModel : IIdentifiableObjectModel
    {
        string PublicationUrl { get; }

        string MultimediaUrl { get; }

        IEnumerable<IFieldModel> Metadata { get; }
    }
}
