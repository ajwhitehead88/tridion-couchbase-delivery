using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    /// <summary>
    /// Models a single component link field
    /// </summary>
    public class ComponentLinkFieldModel : IFieldModel
    {
        public IEnumerable<ComponentModel> Values { get; set; }
    }
}
