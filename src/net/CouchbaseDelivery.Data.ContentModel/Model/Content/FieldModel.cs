using CouchbaseDelivery.Data.ContentModel.Contract.Enums;
using CouchbaseDelivery.Data.ContentModel.Contract.Model.Content;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CouchbaseDelivery.Data.ContentModel.Model.Content
{
    /// <summary>
    /// Models a field and it's values
    /// </summary>
    public class FieldModel : IFieldModel
    {
        public FieldModel(string name, FieldTypes type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; private set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FieldTypes Type { get; private set; }

        public IEnumerable<string> StringValues { get; set; }

        public IEnumerable<double> NumberValues { get; set; }

        public IEnumerable<DateTime> DateTimeValues { get; set; }

        public IEnumerable<KeywordModel> KeywordValues { get; set; }

        public IEnumerable<IEnumerable<FieldModel>> EmbeddedValues { get; set; }

        public IEnumerable<ComponentModel> ComponentLinkValues { get; set; }

        [JsonIgnore]
        IEnumerable<IKeywordModel> IFieldModel.KeywordValues { get { return KeywordValues; } }

        [JsonIgnore]
        IEnumerable<IEnumerable<IFieldModel>> IFieldModel.EmbeddedValues { get { return EmbeddedValues; } }

        [JsonIgnore]
        IEnumerable<IComponentModel> IFieldModel.ComponentLinkValues { get { return ComponentLinkValues; } }
    }
}
