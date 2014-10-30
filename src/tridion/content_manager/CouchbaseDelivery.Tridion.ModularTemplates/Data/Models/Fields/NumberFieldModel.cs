using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    public class NumberFieldModel : IFieldModel
    {
        public IEnumerable<double> Values { get; set; }
    }
}
