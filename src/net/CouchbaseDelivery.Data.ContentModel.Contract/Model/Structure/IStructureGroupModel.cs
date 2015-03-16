using CouchbaseDelivery.Data.ContentModel.Contract.Model.Content;
using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Contract.Model.Structure
{
    /// <summary>
    /// Models a structure group
    /// </summary>
    public interface IStructureGroupModel : IIdentifiableObjectModel
    {
        string PublishedUrl { get; }

        IEnumerable<IFieldModel> Metadata { get; }
    }
}
