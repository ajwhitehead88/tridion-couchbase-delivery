using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    /// <summary>
    /// Models a single number field
    /// </summary>
    public class NumberFieldModel : IFieldModel
    {
        public IEnumerable<double> Values { get; set; }
    }
}
