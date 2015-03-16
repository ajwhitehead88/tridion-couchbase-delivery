using CouchbaseDelivery.Data.ContentModel.Contract.Model.Content;
using CouchbaseDelivery.Data.ContentModel.Contract.Model.Layout;
using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Contract.Model.Structure
{
    /// <summary>
    /// Models a published page
    /// </summary>
    public interface IPageModel : IIdentifiableObjectModel
    {
        string PublishedUrl { get; }

        ITemplateModel TemplateModel { get; }

        IEnumerable<IComponentPresentationModel> ComponentPresentations { get; }

        IEnumerable<IFieldModel> Metadata { get; }
    }
}
