using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Model.Fields
{
    /// <summary>
    /// Models a single number field
    /// </summary>
    public class NumberFieldModel : IFieldModel
    {
        public IEnumerable<double> Values { get; set; }
    }
}
