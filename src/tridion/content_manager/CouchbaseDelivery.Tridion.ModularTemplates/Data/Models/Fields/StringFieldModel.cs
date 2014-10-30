using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    public class StringFieldModel : IFieldModel
    {
        public IEnumerable<string> Values { get; set; }
    }
}
