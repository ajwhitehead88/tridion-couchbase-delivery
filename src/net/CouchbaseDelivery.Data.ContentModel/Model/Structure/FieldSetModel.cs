using System.Collections.Generic;
using CouchbaseDelivery.Data.ContentModel.Model.Fields;

namespace CouchbaseDelivery.Data.ContentModel.Model.Structure
{
    /// <summary>
    /// Models a set of fields
    /// </summary>
    public class FieldSetModel : Dictionary<string, IFieldModel>
    {
    }
}
