using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    public class KeywordFieldModel : IFieldModel
    {
        public IEnumerable<KeywordModel> Values { get; set; }
    }
}
