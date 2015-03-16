using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Contract.Model.Content
{
    public interface IComponentModel : IIdentifiableObjectModel
    {
        IEnumerable<IFieldModel> Content { get; }

        IEnumerable<IFieldModel> Metadata { get; }

        string BinaryUrl { get; }
    }
}
