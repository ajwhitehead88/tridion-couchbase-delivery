using System.Collections.Generic;
using CouchbaseDelivery.Data.ContentModel.Model.Content;

namespace CouchbaseDelivery.Data.ContentModel.Model.Fields
{
    /// <summary>
    /// Models a single keyword field
    /// </summary>
    public class KeywordFieldModel : IFieldModel
    {
        public IEnumerable<KeywordModel> Values { get; set; }
    }
}
