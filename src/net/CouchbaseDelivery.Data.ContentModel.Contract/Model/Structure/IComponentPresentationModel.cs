using CouchbaseDelivery.Data.ContentModel.Contract.Model.Content;
using CouchbaseDelivery.Data.ContentModel.Contract.Model.Layout;

namespace CouchbaseDelivery.Data.ContentModel.Contract.Model.Structure
{
    /// <summary>
    /// Models a single component presentation
    /// </summary>
    public interface IComponentPresentationModel
    {
        IComponentModel ComponentModel { get; }

        ITemplateModel TemplateModel { get; }
    }
}
