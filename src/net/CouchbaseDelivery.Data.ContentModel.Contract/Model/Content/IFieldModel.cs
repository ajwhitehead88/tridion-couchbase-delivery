using CouchbaseDelivery.Data.ContentModel.Contract.Enums;
using System;
using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Contract.Model.Content
{
    public interface IFieldModel
    {
        string Name { get; }

        FieldTypes Type { get; }

        IEnumerable<string> StringValues { get; }

        IEnumerable<double> NumberValues { get; }

        IEnumerable<DateTime> DateTimeValues { get; }

        IEnumerable<IKeywordModel> KeywordValues { get; }

        IEnumerable<IEnumerable<IFieldModel>> EmbeddedValues { get; }

        IEnumerable<IComponentModel> ComponentLinkValues { get; }
    }
}
