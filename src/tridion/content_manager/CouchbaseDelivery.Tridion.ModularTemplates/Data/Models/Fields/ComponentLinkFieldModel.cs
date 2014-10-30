using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    public class ComponentLinkFieldModel : IFieldModel
    {
        public IEnumerable<ComponentModel> Values { get; set; }
    }
}
