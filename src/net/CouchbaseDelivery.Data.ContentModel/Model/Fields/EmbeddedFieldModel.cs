using System.Collections.Generic;
using CouchbaseDelivery.Data.ContentModel.Model.Structure;

namespace CouchbaseDelivery.Data.ContentModel.Model.Fields
{
    /// <summary>
    /// Models a single embedded field
    /// </summary>
    public class EmbeddedFieldModel : IFieldModel
    {
        public IEnumerable<FieldSetModel> Values { get; set; }
    }
}
