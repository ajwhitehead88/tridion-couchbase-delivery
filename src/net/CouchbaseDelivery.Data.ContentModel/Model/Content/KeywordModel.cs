using CouchbaseDelivery.Data.ContentModel.Contract.Model.Content;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Model.Content
{
    /// <summary>
    /// Models a keyword and it's metadata
    /// </summary>
    public class KeywordModel : IdentifiableObjectModel, IKeywordModel
    {
        public string Key { get; set; }

        public IEnumerable<FieldModel> Metadata { get; set; }

        [JsonIgnore]
        IEnumerable<IFieldModel> IKeywordModel.Metadata { get { return Metadata; } }
    }
}
