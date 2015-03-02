using System.Collections.Generic;
using CouchbaseDelivery.Data.ContentModel.Model.Content;

namespace CouchbaseDelivery.Data.ContentModel.Model.Fields
{
    /// <summary>
    /// Models a single component link field
    /// </summary>
    public class ComponentLinkFieldModel : IFieldModel
    {
        public IEnumerable<ComponentModel> Values { get; set; }
    }
}
