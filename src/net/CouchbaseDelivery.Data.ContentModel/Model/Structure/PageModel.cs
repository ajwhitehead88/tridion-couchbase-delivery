using CouchbaseDelivery.Data.ContentModel.Contract.Model.Content;
using CouchbaseDelivery.Data.ContentModel.Contract.Model.Layout;
using CouchbaseDelivery.Data.ContentModel.Contract.Model.Structure;
using CouchbaseDelivery.Data.ContentModel.Model.Content;
using CouchbaseDelivery.Data.ContentModel.Model.Layout;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Model.Structure
{
    /// <summary>
    /// Models a published page
    /// </summary>
    public class PageModel : IdentifiableObjectModel, IPageModel
    {
        public string PublishedUrl { get; set; }

        public TemplateModel TemplateModel { get; set; }

        public IEnumerable<ComponentPresentationModel> ComponentPresentations { get; set; }

        public IEnumerable<FieldModel> Metadata { get; set; }

        [JsonIgnore]
        ITemplateModel IPageModel.TemplateModel { get { return TemplateModel; } }

        [JsonIgnore]
        IEnumerable<IComponentPresentationModel> IPageModel.ComponentPresentations { get { return ComponentPresentations; } }

        [JsonIgnore]
        IEnumerable<IFieldModel> IPageModel.Metadata { get { return Metadata; } } 
    }
}
