using CouchbaseDelivery.Data.ContentModel.Contract.Model.Content;
using CouchbaseDelivery.Data.ContentModel.Contract.Model.Layout;
using CouchbaseDelivery.Data.ContentModel.Contract.Model.Structure;
using CouchbaseDelivery.Data.ContentModel.Model.Content;
using CouchbaseDelivery.Data.ContentModel.Model.Layout;
using Newtonsoft.Json;

namespace CouchbaseDelivery.Data.ContentModel.Model.Structure
{
    /// <summary>
    /// Models a single component presentation
    /// </summary>
    public class ComponentPresentationModel : IComponentPresentationModel
    {
        public ComponentModel ComponentModel { get; set; }

        public TemplateModel TemplateModel { get; set; }

        [JsonIgnore]
        IComponentModel IComponentPresentationModel.ComponentModel { get { return ComponentModel; } }

        [JsonIgnore]
        ITemplateModel IComponentPresentationModel.TemplateModel { get { return TemplateModel; } }
    }
}
