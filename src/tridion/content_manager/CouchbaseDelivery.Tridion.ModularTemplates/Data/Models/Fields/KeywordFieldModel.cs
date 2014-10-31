using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    /// <summary>
    /// Models a single keyword field
    /// </summary>
    public class KeywordFieldModel : IFieldModel
    {
        public IEnumerable<KeywordModel> Values { get; set; }
    }
}
