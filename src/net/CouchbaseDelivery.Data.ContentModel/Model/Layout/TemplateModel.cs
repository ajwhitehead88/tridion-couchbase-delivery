using CouchbaseDelivery.Data.ContentModel.Contract.Model.Content;
using CouchbaseDelivery.Data.ContentModel.Contract.Model.Layout;
using CouchbaseDelivery.Data.ContentModel.Model.Content;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Model.Layout
{
    /// <summary>
    /// Models a template
    /// </summary>
    public class TemplateModel : IdentifiableObjectModel, ITemplateModel
    {
        public int Priority { get; set; }

        public IEnumerable<FieldModel> Metadata { get; set; }

        [JsonIgnore]
        IEnumerable<IFieldModel> ITemplateModel.Metadata { get { return Metadata; } }
    }
}
