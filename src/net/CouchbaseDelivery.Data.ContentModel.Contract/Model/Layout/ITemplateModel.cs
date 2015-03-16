using CouchbaseDelivery.Data.ContentModel.Contract.Model.Content;
using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Contract.Model.Layout
{
    /// <summary>
    /// Models a template
    /// </summary>
    public interface ITemplateModel : IIdentifiableObjectModel
    {
        int Priority { get; }

        IEnumerable<IFieldModel> Metadata { get; }
    }
}
