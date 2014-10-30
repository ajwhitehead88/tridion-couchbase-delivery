using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Layout
{
    public class PageTemplateModel : IdentifiableObjectModel
    {
        public IEnumerable<IFieldModel> Metadata { get; set; }
    }
}
