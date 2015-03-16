
using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Contract.Model.Content
{
    /// <summary>
    /// Models a keyword and it's metadata
    /// </summary>
    public interface IKeywordModel : IIdentifiableObjectModel
    {
        string Key { get; }

        IEnumerable<IFieldModel> Metadata { get; }
    }
}
