using CouchbaseDelivery.Data.ContentModel.Contract.Model.Content;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Model.Content
{
    public class ComponentModel : IdentifiableObjectModel, IComponentModel
    {
        public IEnumerable<FieldModel> Content { get; set; }

        public IEnumerable<FieldModel> Metadata { get; set; }

        public string BinaryUrl { get; set; }

        [JsonIgnore]
        IEnumerable<IFieldModel> IComponentModel.Content { get { return Content; } }

        [JsonIgnore]
        IEnumerable<IFieldModel> IComponentModel.Metadata { get { return Metadata; } }
    }
}
