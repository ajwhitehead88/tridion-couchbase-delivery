using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    public class EmbeddedFieldModel : IFieldModel
    {
        public IEnumerable<FieldSetModel> Values { get; set; }
    }
}
