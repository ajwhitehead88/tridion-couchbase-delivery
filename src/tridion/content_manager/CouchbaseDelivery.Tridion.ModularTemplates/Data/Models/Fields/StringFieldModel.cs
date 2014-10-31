using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    /// <summary>
    /// Models a single text field - this encompases all text field types
    /// </summary>
    public class StringFieldModel : IFieldModel
    {
        public IEnumerable<string> Values { get; set; }
    }
}
